using Events.Domain.Common.Exceptions;
using System.Net;

namespace Events.Domain.Events.Exceptions;

public class NotEventOrganizerException : DomainHttpException
{
    public NotEventOrganizerException() : base("You aren't organizer of event, you can't update it")
    {
        StatusCode = HttpStatusCode.BadRequest;
    }
}