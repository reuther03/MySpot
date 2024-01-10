using MySpot.Core.Exceptions;

namespace MySpot.Application.Exceptions;

public sealed class ReservationNotFoundException : CustomException
{
    public Guid Id { get; }

    public ReservationNotFoundException(Guid id) :
        base($"Reservation with id {id} was not found.")
    {
        Id = id;
    }
}