using Events.Application.Common.Interfaces.Persistance;
using Events.Domain.Users;
using Events.Domain.Users.Exceptions;
using MediatR;

namespace Events.Application.Authentication.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery>
    {
        private readonly IUserRepository _userRepository;

        public LoginQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            User? userFromDb = await _userRepository.GetByEmail(query.Email);

            if (userFromDb is null)
            {
                throw new UserNotExistsException(query.Email);
            }

            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(query.Password, userFromDb.Password);

            if (!isPasswordCorrect)
            {
                throw new IncorrectPasswordException(query.Email);
            }

            return Unit.Value;
            // TODO: generate jwt
            // TODO: return authentication result
        }
    }
}

