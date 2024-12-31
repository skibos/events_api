using MediatR;

namespace Events.Application.Authentication.Queries.Login;

public record LoginQuery(
    string Email,
    string Password) : IRequest;