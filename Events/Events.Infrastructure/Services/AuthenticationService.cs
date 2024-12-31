
using System.Security.Claims;
using System.Text;
using Events.Application.Common.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Events.Infrastructure.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IConfiguration _configuration;

    public AuthenticationService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateJwtToken(Guid userId, string firstName, string lastName)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Jwt:secret")));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var expireDate = DateTime.Now.AddYears(5);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.GivenName, firstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, lastName),
                new Claim(JwtRegisteredClaimNames.Exp, expireDate.ToString()),
                new Claim("user_id", userId.ToString()),
            }),
            Expires = expireDate,
            SigningCredentials = credentials,
            Audience = _configuration.GetValue<string>("Jwt:audience"),
            Issuer = _configuration.GetValue<string>("Jwt:issuer"),
        };

        var handler = new JsonWebTokenHandler();
        var token = handler.CreateToken(tokenDescriptor);

        return token;
    }
}

