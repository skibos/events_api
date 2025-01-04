using Events.Domain.Common.Exceptions;
using System.Net;

namespace Events.Domain.Events.Exceptions;

public class EventNotFoundException : DomainHttpException
{
    public EventNotFoundException() : base("Event not found")
    {
        StatusCode = HttpStatusCode.NotFound;
    }
}
