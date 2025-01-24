using System;
using Events.Application.Common.Interfaces.Persistance;
using Events.Application.Events.Dto;
using Events.Domain.Events;
using Events.Domain.Events.Exceptions;
using MediatR;

namespace Events.Application.Events.Queries.GetEvent;

public class GetEventQueryHandler : IRequestHandler<GetEventQuery, EventResult>
{ 
    private readonly IEventRepository _eventRepository;

    public GetEventQueryHandler(IEventRepository eventRepository)
	{
        _eventRepository = eventRepository;
	}

    public async Task<EventResult> Handle(GetEventQuery query, CancellationToken cancellationToken)
    {
        Event? myEvent = await _eventRepository.GetById(query.EventId);


        if (myEvent is null)
        {
            throw new EventNotFoundException();
        }

        EventResult result = new EventResult(
             myEvent.Id,
             myEvent.Name,
             myEvent.Description,
             myEvent.Location.Latitude,
             myEvent.Location.Longitude,
             myEvent.StartTime,
             myEvent.EndTime,
             myEvent.EventStatus.ToString()
        );

        return result;
    }
}

