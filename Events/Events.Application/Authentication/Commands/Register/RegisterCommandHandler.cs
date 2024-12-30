using Events.Application.Common.Interfaces.Persistance;
using Events.Domain.Users;
using Events.Domain.Users.Exceptions;
using MediatR;

namespace Events.Application.Authentication.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand>
    {
        private readonly IUserRepository _userRepository;

        public RegisterCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            User? userFromDb = await _userRepository.GetByEmail(command.Email);

            if (userFromDb is not null)
            {
                throw new UserAlreadyExistsException(command.Email);
            }

            User newUser = User.Create(command.FirstName, command.LastName, command.Email, BCrypt.Net.BCrypt.HashPassword(command.Password));

            await _userRepository.Add(newUser);

            return Unit.Value;
            // TODO: generate jwt
            // TODO: return authentication result
        }
    }
}

