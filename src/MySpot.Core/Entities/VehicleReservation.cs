using MySpot.Core.ValueObjects;

namespace MySpot.Core.Entities;

public class VehicleReservation : Reservation
{
    public UserId UserId { get; private set; }
    public EmployeeName EmployeeName { get; private set; } = default!;
    public LicencePlate LicencePlate { get; private set; } = default!;

    public void ChangeLicencePlate(LicencePlate licencePlate)
        => LicencePlate = licencePlate;

    private VehicleReservation()
    {
    }

    public VehicleReservation(ReservationId id, UserId userId, EmployeeName employeeName, LicencePlate licencePlate, Capacity capacity, Date date)
        : base(id, capacity, date)
    {
        UserId = userId;
        EmployeeName = employeeName;
        LicencePlate = licencePlate;
    }
}