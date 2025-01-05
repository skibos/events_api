using Events.Application.Common.Dto;
using Events.Application.Events.Dto;
using MediatR;

namespace Events.Application.Events.Queries.GetAllParticipantsByEvent;


public record GetAllParticipantsByEventQuery : PaginationParams, IRequest<PaginatedResult<ParticipantResult>>
{
    public Guid EventId { get; init; }

    public GetAllParticipantsByEventQuery(Guid eventId, int pageNumber, int pageSize) : base(pageNumber, pageSize)
    {
        EventId = eventId;
    }
};