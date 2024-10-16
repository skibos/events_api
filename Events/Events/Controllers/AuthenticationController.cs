using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace Events.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{
    private readonly ISender _mediator;

    public AuthenticationController(ISender mediator)
    {
        _mediator = mediator;
    }

    [Route("register"), HttpPost]
    public async Task<IActionResult> Register()
    {
        return Ok();
    }

    [Route("login"), HttpPost]
    public async Task<IActionResult> Login()
    {
        return Ok();
    }
}

