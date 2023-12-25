using MySpot.Core.ValueObjects;

namespace MySpot.Core.Entities;

public class Reservation
{
    public ReservationId Id { get; } = default!;
    public EmployeeName EmployeeName { get; private set; } = default!;
    public LicencePlate LicencePlate { get; private set; } = default!;
    public Date Date { get; private set; } = default!;

    /// <summary>
    /// For EF Core
    /// <list type="number">
    /// <item><description>EF Core needs parameterless constructor</description></item>
    /// </list>
    /// </summary>
    private Reservation()
    {
    }

    public Reservation(ReservationId id, EmployeeName employeeName, LicencePlate licencePlate, Date date)
    {
        Id = id;
        EmployeeName = employeeName;
        LicencePlate = licencePlate;
        Date = date;
    }

    public void ChangeLicencePlate(LicencePlate licencePlate)
        => LicencePlate = licencePlate;
}