﻿using System.Data;
using Events.Domain.Common.Models;
using Events.Domain.Events.Entities;
using Events.Domain.Events.Enums;
using Events.Domain.Events.Exceptions;
using Events.Domain.Events.ValueObjects;
using Events.Domain.Users.ValueObjects;

namespace Events.Domain.Events;

public sealed class Event : AggregateRoot<EventId, Guid>
{
	private readonly List<Participant> _participants = new();

	public string Name { get; private set; }
	public string Description { get; private set; }
	public Location Location { get; private set; }
	public EventStatus EventStatus { get; private set; }
	public IReadOnlyList<Participant> Participants => _participants.AsReadOnly();

	public Event(
		EventId id,
		string name,
		string description,
		Location location) : base(id)
	{
		Name = name;
		Description = description;
		Location = location;
		EventStatus = EventStatus.Scheduled;
	}

	public static Event Create(
		UserId eventCreatorId,
		string name,
		string description,
		Location location
	)
	{
		Event newEvent = new(
			EventId.CreateUnique(),
			name,
			description,
			location
		);

		newEvent.AddParticipant(new Participant(eventCreatorId, ParticipantRole.Organizer, AttendanceStatus.Confirmed));

		return newEvent;
	}

    public void AddParticipant(Participant participant)
    {
        if (_participants.Any(p => p.Id == participant.Id))
        {
			throw new UserIsAlreadyParticipantOfEventException();
        }

        _participants.Add(participant);
    }

    public void ChangeParticipantRole(UserId participantId, ParticipantRole newRole)
    {
		var participant = GetParticipantOfEvent(participantId);

        participant.Role = newRole;
    }

    public void ChangeParticipantAttendanceStatus(UserId participantId, AttendanceStatus newAttendaceStatus)
    {
        var participant = GetParticipantOfEvent(participantId);

        participant.AttendanceStatus = newAttendaceStatus;
    }

	private Participant GetParticipantOfEvent(UserId participantId)
    {
		return _participants.FirstOrDefault(p => p.Id == participantId) ?? throw new UserIsNotParticipantOfEventException();
    }
}

