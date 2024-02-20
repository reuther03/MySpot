using MySpot.Core.Exceptions;
using MySpot.Core.ValueObjects;

namespace MySpot.Core.Entities;

internal class ParkingSpotCapacityExceededException : CustomException
{
    private readonly ParkingSpotId _id;

    public ParkingSpotCapacityExceededException(ParkingSpotId id)
        : base(message: $"Parking spot capacity exceeded: {id}")
    {
        _id = id;
    }
}