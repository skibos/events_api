using System;
using System.Net;
using Events.Domain.Common.Exceptions;

namespace Events.Domain.Events.Exceptions
{
	public class UserIsNotParticipantOfEventException : DomainHttpException
	{
        public UserIsNotParticipantOfEventException() : base("User not exists in event participants list")
        {
            StatusCode = HttpStatusCode.NotFound;
        }
    }
}

