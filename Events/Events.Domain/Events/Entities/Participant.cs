using Events.Domain.Common.Models;
using Events.Domain.Events.Enums;
using Events.Domain.Events.ValueObjects;
using Events.Domain.Users.ValueObjects;

namespace Events.Domain.Events.Entities;

public class Participant : Entity<ParticipantId>
{
    public UserId UserId { get; set; }
    public ParticipantRole Role { get; set; }
	public AttendanceStatus AttendanceStatus { get; set; }

    #pragma warning disable CS8618
    private Participant() { }
    #pragma warning restore CS8618

    public Participant(ParticipantId id, UserId userId, ParticipantRole role, AttendanceStatus attendanceStatus) : base(id)
	{
        UserId = userId;
		Role = role;
		AttendanceStatus = attendanceStatus;
    }
}

