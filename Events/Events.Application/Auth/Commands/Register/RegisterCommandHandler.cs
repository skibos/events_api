using Events.Application.Authentication.Dto;
using Events.Application.Common.Interfaces.Persistance;
using Events.Application.Common.Interfaces.Services;
using Events.Domain.Users;
using Events.Domain.Users.Exceptions;
using MediatR;

namespace Events.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthenticationResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthenticationService _authenticationService;

    public RegisterCommandHandler(IUserRepository userRepository, IAuthenticationService authenticationService)
    {
        _userRepository = userRepository;
        _authenticationService = authenticationService;
    }

    public async Task<AuthenticationResult> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        User? userFromDb = await _userRepository.GetByEmail(command.Email);

        if (userFromDb is not null)
        {
            throw new UserAlreadyExistsException(command.Email);
        }

        User newUser = User.Create(command.FirstName, command.LastName, command.Email, BCrypt.Net.BCrypt.HashPassword(command.Password));
        await _userRepository.Add(newUser);

        string token = _authenticationService.GenerateJwtToken(newUser.Id.Value, newUser.FirstName, newUser.LastName);

        return new AuthenticationResult(token, newUser.Id.Value);
    }
}

