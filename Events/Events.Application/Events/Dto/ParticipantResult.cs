
using Events.Domain.Events.Enums;

namespace Events.Application.Events.Dto;

public record ParticipantResult(
    Guid EventId,
    Guid UserId,
    string FirstName,
    string LastName,
    string Email,
    string Role,
    string AttendanceStatus
);
