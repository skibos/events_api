using System.Net;
using Events.Domain.Common.Exceptions;

namespace Events.Domain.Events.Exceptions;

public class StartTimeBeforeNowException : DomainHttpException
{
	public StartTimeBeforeNowException() : base("Start time cannot be before current time")
	{
		StatusCode = HttpStatusCode.BadRequest;
	}
}

