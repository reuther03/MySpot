﻿using MySpot.Api.Exceptions;

namespace MySpot.Api.ValueObjects;

public record ReservationId
{
    public Guid Value { get; }

    public ReservationId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new InvalidEntityIdException(value);
        }

        Value = value;
    }

    private static ReservationId Create() => new(Guid.NewGuid());

    public static implicit operator Guid(ReservationId date)
        => date.Value;

    public static implicit operator ReservationId(Guid value)
        => new(value);
}