using System.Collections.Generic;
using Events.Application.Common.Dto;
using Events.Application.Common.Interfaces.Persistance;
using Events.Application.Events.Dto;
using Events.Domain.Events;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Events.Application.Events.Queries.GetAllEvents;

public class GetAllEventsQueryHandler : IRequestHandler<GetAllEventsQuery, PaginatedResult<EventResult>>
{
    private readonly IEventRepository _eventRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetAllEventsQueryHandler(IEventRepository eventRepository, IHttpContextAccessor httpContextAccessor)
	{
        _eventRepository = eventRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<PaginatedResult<EventResult>> Handle(GetAllEventsQuery query, CancellationToken cancellationToken)
    {
        string? userId = _httpContextAccessor.HttpContext?.User.FindFirst("user_id")?.Value;

        if (userId is null)
        {
            throw new UnauthorizedAccessException();
        }

        PaginatedResult<Event> myEvents = await _eventRepository.GetPagedEvents(Guid.Parse(userId), query.PageNumber, query.PageSize);
        List<EventResult> eventResultList = myEvents.Items.Select(e => new EventResult(
            e.Id.Value,
            e.Name,
            e.Description,
            e.Location.Latitude,
            e.Location.Longitude,
            e.StartTime,
            e.EndTime,
            e.EventStatus.ToString()
        )).ToList();

        PaginatedResult<EventResult> result = new(
            eventResultList,
            myEvents.Empty,
            myEvents.CurrentPage,
            myEvents.ResultsPerPage,
            myEvents.TotalPages,
            myEvents.TotalResults
        );

        return result;
    }
}

