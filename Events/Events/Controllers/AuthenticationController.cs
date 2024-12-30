﻿using Microsoft.AspNetCore.Mvc;
using MediatR;
using Events.API.Dto.Auth;
using Events.Application.Authentication.Commands.Register;
using Events.Domain.Users.Exceptions;

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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(UserAlreadyExistsException))]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        RegisterCommand registerCommand = new (FirstName: request.FirstName, LastName: request.LastName, Email: request.Email, Password: request.Password);

        await _mediator.Send(registerCommand);

        return NoContent();
    }

    [Route("login"), HttpPost]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        return Ok();
    }
}

