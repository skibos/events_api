
using Events.Application.Events.Dto;
using MediatR;

namespace Events.Application.Events.Commands.CreateEvent;

public record CreateEventCommand(
    string Name,
    string Description,
    double Latitude,
    double Longitude,
    DateTime StartTime,
    DateTime EndTime) : IRequest<EventResult>;