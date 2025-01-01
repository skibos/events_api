using Events.Domain.Common.Models;
using Events.Domain.Events.Enums;
using Events.Domain.Users.ValueObjects;

namespace Events.Domain.Events.Entities;

public class Participant : Entity<UserId>
{
    public ParticipantRole Role { get; set; }
	public AttendanceStatus AttendanceStatus { get; set; }
    public DateTime JoinedDateTime { get; private set; }

    public Participant(UserId id, ParticipantRole role, AttendanceStatus attendanceStatus) : base(id)
	{
		Role = role;
		AttendanceStatus = attendanceStatus;
        JoinedDateTime = DateTime.UtcNow;
    }
}

