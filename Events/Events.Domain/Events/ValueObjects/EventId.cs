using Events.Domain.Common.Models;

namespace Events.Domain.Events.ValueObjects;

public sealed class EventId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private EventId(Guid value)
    {
        Value = value;
    }

    public static EventId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static EventId Create(Guid value)
    {
        return new(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator Guid(EventId data)
        => data.Value;
}

