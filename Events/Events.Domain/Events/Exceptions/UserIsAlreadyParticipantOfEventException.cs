using System.Net;
using Events.Domain.Common.Exceptions;

namespace Events.Domain.Events.Exceptions;

public class UserIsAlreadyParticipantException : DomainHttpException
{
	public UserIsAlreadyParticipantException() : base("User already exists in events participants list")
	{
        StatusCode = HttpStatusCode.Conflict;
    }
}

