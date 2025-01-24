using Events.Application.Common.Interfaces.Persistance;
using Events.Domain.Events;
using Events.Domain.Events.Enums;
using Events.Domain.Events.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Events.Application.Events.Commands.CancelEvent;

public class CancelEventCommandHandler : IRequestHandler<CancelEventCommand>
{
    private readonly IEventRepository _eventRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CancelEventCommandHandler(IEventRepository eventRepository, IHttpContextAccessor httpContextAccessor)
	{
        _eventRepository = eventRepository;
        _httpContextAccessor = httpContextAccessor;
	}

    public async Task<Unit> Handle(CancelEventCommand command, CancellationToken cancellationToken)
    {
        string? userId = _httpContextAccessor.HttpContext?.User.FindFirst("user_id")?.Value;

        if (userId is null)
        {
            throw new UnauthorizedAccessException();
        }

        Event? myEvent = await _eventRepository.GetById(command.EventId);

        if (myEvent is null)
        {
            throw new EventNotFoundException();
        }

        if (!myEvent.Participants.Any(p => p.UserId.Value == Guid.Parse(userId) && p.Role == ParticipantRole.Organizer))
        {
            throw new NotEventOrganizerException();
        }

        myEvent.ChangeEventStatus(EventStatus.Canceled);

        await _eventRepository.SaveChangesAsync();

        return Unit.Value;
    }
}

