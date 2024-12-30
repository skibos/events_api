using Events.Application.Common.Interfaces.Persistance;
using Events.Domain.Users;
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
            // TODO: check if user exists in db

            User user = User.Create(command.FirstName, command.LastName, command.Email, command.Password);

            await _userRepository.Add(user);

            return Unit.Value;
            // TODO: generate jwt
            // TODO: return authentication result
        }
    }
}

