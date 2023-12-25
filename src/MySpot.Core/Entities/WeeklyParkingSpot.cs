using MySpot.Core.Exceptions;
using MySpot.Core.ValueObjects;

namespace MySpot.Core.Entities;

public class WeeklyParkingSpot
{
    public ParkingSpotId Id { get; } = default!;
    public Week Week { get; private set; } = default!;
    public ParkingSpotName Name { get; private set; } = default!;
    public IEnumerable<Reservation> Reservations => _reservations;

    private readonly HashSet<Reservation> _reservations = new();

    private WeeklyParkingSpot()
    {
    }

    public WeeklyParkingSpot(Guid id, Week week, string name)
    {
        Id = id;
        Week = week;
        Name = name;
    }

    public void AddReservation(Reservation reservation, Date now)
    {
        var isInvalidDate = reservation.Date.DateOnly() < Week.From.DateOnly() ||
                            reservation.Date.DateOnly() > Week.To.DateOnly() ||
                            reservation.Date.DateOnly() < now.DateOnly();
        if (isInvalidDate)
        {
            throw new InvalidReservationDateException(reservation.Date.Value.Date);
        }

        var alreadyReserved = _reservations.Any(x => x.Date == reservation.Date);
        if (alreadyReserved)
        {
            throw new ParkingSpotAlreadyReservedException(Name, reservation.Date.Value.Date);
        }

        _reservations.Add(reservation);
    }

    public void RemoveReservation(ReservationId id)
        => _reservations.RemoveWhere(x => x.Id == id);
}