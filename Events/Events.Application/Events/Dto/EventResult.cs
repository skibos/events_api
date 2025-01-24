namespace Events.Application.Events.Dto;

public record EventResult(
    Guid EventId,
    string Name,
    string Description,
    double Latitude,
    double Longitude,
    DateTime StartTime,
    DateTime EndTime,
    string EventStatus
);

// TODO: add organizers
