using System;
using Events.Domain.Common.Exceptions;
using System.Net;

namespace Events.Domain.Users.Exceptions
{
    public class UserNotExistsException : DomainHttpException
    {
        public UserNotExistsException()
            : base("User not exists.")
        {
            StatusCode = HttpStatusCode.NotFound;
        }
    }
}

