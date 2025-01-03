using Events.Application.Common.Interfaces.Persistance;
using Events.Application.Events.Dto;
using Events.Domain.Events;
using Events.Domain.Events.ValueObjects;
using Events.Domain.Users.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Events.Application.Events.Commands.CreateEvent;

public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, EventResult>
{
    private readonly IEventRepository _eventRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateEventCommandHandler(IEventRepository eventRepository, IHttpContextAccessor httpContextAccessor)
	{
        _eventRepository = eventRepository;
        _httpContextAccessor = httpContextAccessor;
	}

    public async Task<EventResult> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        string? userId = _httpContextAccessor.HttpContext?.User.FindFirst("user_id")?.Value;

        if (userId is null)
        {
            throw new UnauthorizedAccessException();
        }

        Location location = Location.CreateNew(request.Latitude, request.Longitude);

        Event newEvent = Event.Create(
            UserId.Create(Guid.Parse(userId)),
            request.Name,
            request.Description,
            location,
            request.StartTime,
            request.EndTime
        );

        await _eventRepository.Add(newEvent);

        return new EventResult(newEvent.Id.Value);
    }
}

