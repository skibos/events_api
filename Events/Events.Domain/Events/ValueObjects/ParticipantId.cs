using System;
using Events.Domain.Common.Models;

namespace Events.Domain.Events.ValueObjects;

public sealed class ParticipantId : ValueObject
{
    public Guid Value { get; }

    private ParticipantId(Guid value)
    {
        Value = value;
    }

    public static ParticipantId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static ParticipantId Create(Guid value)
    {
        return new(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    private ParticipantId() { }
}
