using MySpot.Core.Exceptions;

namespace MySpot.Core.ValueObjects;

public sealed class InvalidCapacityException : CustomException
{
    public int Capacity { get; }
    public InvalidCapacityException(int capacity)
        : base(message: $"Invalid capacity: {capacity}")
    {
        Capacity = capacity;
    }
}