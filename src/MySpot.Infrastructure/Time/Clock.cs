using MySpot.Core.Abstractions;

namespace MySpot.Infrastructure.Time;

public sealed class Clock : IClock
{
    public DateTime Current() => DateTime.UtcNow.AddHours(1);
}