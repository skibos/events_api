using Events.Domain.Common.Exceptions;
using System.Net;

namespace Events.Domain.Users.Exceptions
{
    public class IncorrectPasswordException : DomainHttpException
    {
        public string Email { get; }

        public IncorrectPasswordException(string email)
            : base($"Incorrent password for email '{email}' was given.")
        {
            StatusCode = HttpStatusCode.Unauthorized;
            Email = email;
        }
    }
}

