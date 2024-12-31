namespace Events.Application.Authentication.Dto;

public record AuthenticationResult(
    string AuthToken,
    Guid UserId);
