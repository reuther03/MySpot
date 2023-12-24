namespace MySpot.Application.DTO;

public class ReservationDto
{
    public Guid Id { get; set; }
    public string EmployeeName { get; set; }
    public string LicencePlate { get; set; }
    public DateTime Date { get; set; }

    // public static ReservationDto FromEntity(Reservation reservation)
    //     => new()
    //     {
    //         Id = reservation.Id,
    //         EmployeeName = reservation.EmployeeName,
    //         LicencePlate = reservation.LicensePlate.Value,
    //         Date = reservation.Date
    //     };
}