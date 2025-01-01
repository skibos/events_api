using Events.Domain.Common.Models;

namespace Events.Domain.Events.ValueObjects;

public class Location : ValueObject
{
    public double Latitude { get; }
    public double Longitude { get; }

    public Location(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    public static Location CreateNew(
    double latitude,
    double longitude)
    {
        return new(
            latitude,
            longitude);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Latitude;
        yield return Longitude;
    }
}

