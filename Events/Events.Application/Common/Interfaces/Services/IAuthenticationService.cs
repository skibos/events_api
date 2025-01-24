namespace Events.Application.Common.Interfaces.Services;

public interface IAuthenticationService
{
	string GenerateJwtToken(Guid userId, string firstName, string lastName);
}