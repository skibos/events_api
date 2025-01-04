using Events.Application.Common.Dto;
using Events.Application.Events.Dto;
using MediatR;

namespace Events.Application.Events.Queries.GetAllEvents;

public record GetAllEventsQuery : PaginationParams, IRequest<PaginatedResult<EventResult>>
{
    public GetAllEventsQuery(int pageNumber, int pageSize): base(pageNumber, pageSize)
    {
    }
};