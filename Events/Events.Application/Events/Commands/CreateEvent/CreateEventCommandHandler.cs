using Events.Application.Common.Interfaces.Persistance;
using Events.Application.Common.Interfaces.Workers;
using Events.Application.Events.Dto;
using Events.Domain.Events;
using Events.Domain.Events.ValueObjects;
using Events.Domain.Users.ValueObjects;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Events.Application.Events.Commands.CreateEvent;

public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, EventCreatedResult>
{
    private readonly IEventRepository _eventRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly EventWorker _eventWorker;

    public CreateEventCommandHandler(IEventRepository eventRepository, IHttpContextAccessor httpContextAccessor, EventWorker eventWorker)
	{
        _eventRepository = eventRepository;
        _httpContextAccessor = httpContextAccessor;
        _eventWorker = eventWorker;
	}

    public async Task<EventCreatedResult> Handle(CreateEventCommand command, CancellationToken cancellationToken)
    {
        string? userId = _httpContextAccessor.HttpContext?.User.FindFirst("user_id")?.Value;

        if (userId is null)
        {
            throw new UnauthorizedAccessException();
        }

        Location location = Location.CreateNew(command.Latitude, command.Longitude);

        Event newEvent = Event.Create(
            UserId.Create(Guid.Parse(userId)),
            command.Name,
            command.Description,
            location,
            command.StartTime,
            command.EndTime
        );

        await _eventRepository.Add(newEvent);

        _eventWorker.ScheduleChangeEventStatusToOngoingJob(newEvent);
        _eventWorker.ScheduleChangeEventStatusToCompletedJob(newEvent);

        return new EventCreatedResult(newEvent.Id.Value);
    }
}

