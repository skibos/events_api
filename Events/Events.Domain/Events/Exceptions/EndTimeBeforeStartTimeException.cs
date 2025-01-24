using System;
using System.Net;
using Events.Domain.Common.Exceptions;

namespace Events.Domain.Events.Exceptions;

public class EndTimeBeforeStartTimeException : DomainHttpException
{
	public EndTimeBeforeStartTimeException() : base("End time cannot be before start time")
	{
		StatusCode = HttpStatusCode.BadRequest;
	}
}

