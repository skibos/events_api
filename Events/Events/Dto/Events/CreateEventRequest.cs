namespace Events.API.Dto.Events;

public record CreateEventRequest(
    string Name,
    string Description,
    double Latitude,
    double Longitude,
    DateTime StartTime,
    DateTime EndTime);