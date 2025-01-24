using System.Net;
using Events.Domain.Common.Exceptions;

namespace Events.Domain.Users.Exceptions
{
	public class UserAlreadyExistsException : DomainHttpException
    {
        public string Email { get; }

        public UserAlreadyExistsException(string email)
            : base($"A user with the given email '{email}' already exists.")
        {
            StatusCode = HttpStatusCode.Conflict;
            Email = email;
        }
    }
}

