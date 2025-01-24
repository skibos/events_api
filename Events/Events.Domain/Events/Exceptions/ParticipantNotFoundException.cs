using Events.Domain.Common.Exceptions;
using System.Net;

namespace Events.Domain.Events.Exceptions;

public class ParticipantNotFoundException : DomainHttpException
{
    public ParticipantNotFoundException() : base("Participant not found")
    {
        StatusCode = HttpStatusCode.NotFound;
    }
}


