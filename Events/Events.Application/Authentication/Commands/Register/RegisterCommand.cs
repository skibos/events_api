using Events.Application.Authentication.Dto;
using MediatR;

namespace Events.Application.Authentication.Commands.Register;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : IRequest<AuthenticationResult>;