namespace MySpot.Api.Exceptions;

public class InvalidReservationDateException : CustomException
{
    public DateTime Date { get; }

    public InvalidReservationDateException(DateTime date)
        : base($"Invalid reservation date: {date}")
    {
        Date = date;
    }
}