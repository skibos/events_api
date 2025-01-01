using System;
using System.Net;
using Events.Domain.Common.Exceptions;

namespace Events.Domain.Events.Exceptions
{
	public class UserIsNotParticipantOfEventException : DomainHttpException
	{
        public UserIsNotParticipantOfEventException() : base("User already exists in events participants list")
        {
            StatusCode = HttpStatusCode.NotFound;
        }
    }
}

