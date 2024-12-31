using System;
using Events.Domain.Common.Exceptions;
using System.Net;

namespace Events.Domain.Users.Exceptions
{
    public class UserNotExistsException : DomainHttpException
    {
        public string Email { get; }

        public UserNotExistsException(string email)
            : base($"A user with the given email '{email}' not exists.")
        {
            StatusCode = HttpStatusCode.NotFound;
            Email = email;
        }
    }
}

