using MySpot.Core.ValueObjects;

namespace MySpot.Core.Entities;

public abstract class Reservation
{
    public ReservationId Id { get; } = default!;
    public Capacity Capacity { get; private set; } = default!;
    public Date Date { get; private set; } = default!;

    /// <summary>
    /// For EF Core
    /// <list type="number">
    /// <item><description>EF Core needs parameterless constructor</description></item>
    /// </list>
    /// </summary>
    protected Reservation()
    {
    }

    protected Reservation(ReservationId id, Capacity capacity, Date date)
    {
        Id = id;
        Capacity = capacity;
        Date = date;
    }
}