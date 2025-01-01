using System.Net;
using Events.Domain.Common.Exceptions;

namespace Events.Domain.Events.Exceptions;

public class UserIsAlreadyParticipantOfEventException : DomainHttpException
{
	public UserIsAlreadyParticipantOfEventException() : base("User already exists in event participants list")
	{
        StatusCode = HttpStatusCode.Conflict;
    }
}

