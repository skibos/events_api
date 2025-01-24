using Events.Application.Events.Dto;
using MediatR;

namespace Events.Application.Events.Queries.GetParticipantByEvent;

public record GetParticipantByEventQuery(Guid EventId, Guid UserId) : IRequest<ParticipantResult>;