﻿using Microsoft.AspNetCore.Mvc;
using MediatR;
using Events.API.Dto.Auth;
using Events.Application.Authentication.Commands.Register;
using Events.Domain.Users.Exceptions;
using Events.Application.Authentication.Queries.Login;
using Events.Application.Authentication.Dto;

namespace Events.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly ISender _mediator;

    public AuthController(ISender mediator)
    {
        _mediator = mediator;
    }

    [Route("register"), HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(UserAlreadyExistsException))]
    public async Task<IActionResult> RegisterUser(RegisterRequest request)
    {
        RegisterCommand registerCommand = new (FirstName: request.FirstName, LastName: request.LastName, Email: request.Email, Password: request.Password);

        AuthenticationResult result = await _mediator.Send(registerCommand);

        return Ok(result);
    }

    [Route("login"), HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(IncorrectPasswordException))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(UserNotExistsException))]
    public async Task<IActionResult> LoginUser(LoginRequest request)
    {
        LoginQuery loginQuery = new (Email: request.Email, Password: request.Password);

        AuthenticationResult result = await _mediator.Send(loginQuery);

        return Ok(result);
    }
}

