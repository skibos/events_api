using Events.Application.Authentication.Dto;
using Events.Application.Common.Interfaces.Persistance;
using Events.Application.Common.Interfaces.Services;
using Events.Domain.Users;
using Events.Domain.Users.Exceptions;
using MediatR;

namespace Events.Application.Authentication.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthenticationResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationService _authenticationService;

        public LoginQueryHandler(IUserRepository userRepository, IAuthenticationService authenticationService)
        {
            _userRepository = userRepository;
            _authenticationService = authenticationService;
        }

        public async Task<AuthenticationResult> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            User? userFromDb = await _userRepository.GetByEmail(query.Email);

            if (userFromDb is null)
            {
                throw new UserNotExistsException();
            }

            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(query.Password, userFromDb.Password);

            if (!isPasswordCorrect)
            {
                throw new IncorrectPasswordException(query.Email);
            }

            string token = _authenticationService.GenerateJwtToken(userFromDb.Id.Value, userFromDb.FirstName, userFromDb.LastName);

            return new AuthenticationResult(token, userFromDb.Id.Value);
        }
    }
}

