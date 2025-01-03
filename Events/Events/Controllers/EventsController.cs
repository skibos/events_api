using Events.API.Dto.Events;
using Events.Application.Events.Commands.CreateEvent;
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
        return Ok();
    }

    [HttpGet]
    [Route("{eventId}/participants")]
    public async Task<IActionResult> GetAllParticipantsByEvent(string eventId)
    {
        return Ok();
    }

    [HttpGet]
    [Route("{eventId}/participants/{participantId}")]
    public async Task<IActionResult> GetParticipantByEvent(string eventId, string participantId)
    {
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> CreateEvent(CreateEventRequest request)
    {
        CreateEventCommand command = new(request.Name, request.Description, request.Latitude, request.Longitude, request.StartTime, request.EndTime);

        await _mediator.Send(command);

        return Ok();
    }

    [HttpPatch]
    [Route("{eventId}/cancel")]
    public async Task<IActionResult> CancelEvent(string eventId)
    {
        return Ok();
    }
}

