using MySpot.Application.Services;

namespace MySpot.Infrastructure.Time;

public sealed class Clock : IClock
{
    public DateTime Current() => DateTime.UtcNow;
}