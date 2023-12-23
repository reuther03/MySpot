using MySpot.Api.Services;

namespace MySpot.Tests.Unit.Shared;

internal sealed class TestClock : IClock
{
    public DateTime Current()
        => new(2023, 12, 23);
}