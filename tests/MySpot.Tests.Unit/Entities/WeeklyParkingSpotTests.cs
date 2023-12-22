using FluentAssertions;
using MySpot.Api.Entities;
using MySpot.Api.Exceptions;
using MySpot.Api.ValueObjects;

namespace MySpot.Tests.Unit.Entities;

public class WeeklyParkingSpotTests
{
    [Theory]
    [InlineData("2020-02-02")]
    [InlineData("2025-02-02")]
    [InlineData("2023-12-21")]
    public void given_invalid_date_add_reservation_should_fail(string dateString)
    {
        var invalidDate = DateTime.Parse(dateString);

        // Arrange
        var reservation = new Reservation(Guid.NewGuid(), "John Doe",
            "abc-123", new Date(invalidDate));
        // Act
        var exception = Record.Exception(() => _weeklyParkingSpot.AddReservation(reservation, _now));

        // Assert
        exception.Should().NotBeNull();
        // Assert.NotNull(exception);
        exception.Should().BeOfType<InvalidReservationDateException>();
        // Assert.IsType<InvalidReservationDateException>(exception);
    }

    [Fact]
    public void given_reservation_for_already_existing_date_add_reservation_should_fail()
    {
        // Arrange
        var reservationDate = _now.AddDays(1);
        var reservation = new Reservation(Guid.NewGuid(), "John Doe",
            "abc-123", reservationDate);
        _weeklyParkingSpot.AddReservation(reservation, reservationDate);

        // Act
        var exception = Record.Exception(() => _weeklyParkingSpot.AddReservation(reservation, reservationDate));

        // Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<ParkingSpotAlreadyReservedException>();
    }

    [Fact]
    public void given_reservation_for_not_taken_date_add_reservation_should_succeed()
    {
        // Arrange
        var reservationDate = _now.AddDays(1);
        var reservation = new Reservation(Guid.NewGuid(), "John Doe",
            "abc-123", reservationDate);

        // Act
        _weeklyParkingSpot.AddReservation(reservation, reservationDate);

        // Assert
        _weeklyParkingSpot.Reservations.Should().ContainSingle();
        _weeklyParkingSpot.Reservations.Should().Contain(reservation);
    }


    #region ARRANGE

    private readonly WeeklyParkingSpot _weeklyParkingSpot;
    private readonly Date _now;

    public WeeklyParkingSpotTests()
    {
        _now = new Date(DateTime.Parse("2023-12-22"));
        _weeklyParkingSpot = new WeeklyParkingSpot(Guid.NewGuid(), new Week(_now), "P1");
    }

    #endregion
}