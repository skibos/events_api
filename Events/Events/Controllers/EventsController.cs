using Events.API.Dto.Events;
using Events.Application.Events.Commands.CancelEvent;
using Events.Application.Events.Commands.CreateEvent;
using Events.Application.Events.Dto;
using Events.Application.Events.Queries.GetEvent;
using Events.Application.Events.Queries.GetParticipantByEvent;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Events.API.Controllers;

[ApiController]
[Authorize]
[Route("events")]
public class EventsController : ControllerBase
{
    private readonly ISender _mediator;

    public EventsController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllEvents()
    {
        return Ok();
    }

    [HttpGet]
    [Route("{eventId}")]
    public async Task<IActionResult> GetEvent(string eventId)
    {
        GetEventQuery query = new GetEventQuery(Guid.Parse(eventId));

        EventResult result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpGet]
    [Route("{eventId}/participants")]
    public async Task<IActionResult> GetAllParticipantsByEvent(string eventId)
    {
        return Ok();
    }

    [HttpGet]
    [Route("{eventId}/participants/{userId}")]
    public async Task<IActionResult> GetParticipantByEvent(string eventId, string userId)
    {
        GetParticipantByEventQuery query = new GetParticipantByEventQuery(Guid.Parse(eventId), Guid.Parse(userId));

        ParticipantResult result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEvent(CreateEventRequest request)
    {
        CreateEventCommand command = new(request.Name, request.Description, request.Latitude, request.Longitude, request.StartTime, request.EndTime);

        EventCreatedResult result = await _mediator.Send(command);

        return Ok(result);
    }

    [HttpPatch]
    [Route("{eventId}/cancel")]
    public async Task<IActionResult> CancelEvent(string eventId)
    {
        CancelEventCommand command = new(Guid.Parse(eventId));

        await _mediator.Send(command);

        return Ok();
    }
}

