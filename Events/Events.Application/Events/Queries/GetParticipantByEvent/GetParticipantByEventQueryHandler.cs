using System;
using Events.Application.Common.Interfaces.Persistance;
using Events.Application.Events.Dto;
using Events.Domain.Events;
using Events.Domain.Events.Entities;
using Events.Domain.Events.Exceptions;
using Events.Domain.Users;
using Events.Domain.Users.Exceptions;
using MediatR;

namespace Events.Application.Events.Queries.GetParticipantByEvent;

public class GetParticipantByEventHandler : IRequestHandler<GetParticipantByEventQuery, ParticipantResult>
{

	private readonly IEventRepository _eventRepository;
	private readonly IUserRepository _userRepository;

	public GetParticipantByEventHandler(IEventRepository eventRepository, IUserRepository userRepository)
	{
		_eventRepository = eventRepository;
        _userRepository = userRepository;
	}

	public async Task<ParticipantResult> Handle(GetParticipantByEventQuery query, CancellationToken cancellationToken)
	{
        Event? myEvent = await _eventRepository.GetById(query.EventId);


        if (myEvent is null)
        {
            throw new EventNotFoundException();
        }

		Participant? participant = myEvent.Participants.FirstOrDefault(p => p.UserId == query.UserId);

        if (participant is null)
        {
            throw new ParticipantNotFoundException();
        }

        User? userDetails = await _userRepository.GetById(query.UserId);

        if (userDetails is null)
        {
            throw new UserNotExistsException();
        }

        ParticipantResult result = new ParticipantResult(
            query.EventId,
            participant.UserId.Value,
            userDetails.FirstName,
            userDetails.LastName,
            userDetails.Email,
            participant.Role.ToString(),
            participant.AttendanceStatus.ToString()
        );

        return result;
    }
}

