namespace Events.API.Dto.Auth;

public record LoginRequest(
    string Email,
    string Password);