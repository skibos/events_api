using Events.Application.Events.Dto;
using MediatR;

namespace Events.Application.Events.Queries.GetEvent;

public record GetEventQuery(Guid EventId) : IRequest<EventResult>;

