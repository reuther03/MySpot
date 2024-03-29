﻿using MySpot.Application.Commands;
using MySpot.Core.Abstractions;
using MySpot.Core.Repositories;
using MySpot.Infrastructure.DAL.Repository;
using MySpot.Tests.Unit.Shared;

namespace MySpot.Tests.Unit.Services;

public class ReservationServiceTests
{
    // [Fact]
    // public async Task given_valid_command_create_should_add_reservation()
    //  {
    //     //ARRANGE
    //     //%TODO: zoba to
    //         // nie dziala nie wiem dlaczego
    //         // var command = new CreateReservation(Guid.Parse("00000000-0000-0000-0000-000000000001"),
    //         //     Guid.NewGuid(), "John Doe", "abc-123", DateTime.UtcNow.AddDays(1));
    //      var command = new ReserveParkingSpotForVehicle(Guid.Parse("00000000-0000-0000-0000-000000000001"),
    //          Guid.NewGuid(), "John Doe", "abc-123", _clock.Current().AddDays(1));
    //     //ACT
    //     var reservationId = await _reservationsService.ReserveForVehicleAsync(command);
    //
    //     //ASSERT
    //     reservationId.Should().NotBeNull();
    //     reservationId.Value.Should().Be(command.ReservationId);
    // }
    //
    // [Fact]
    // public async Task given_invalid_parking_spot_id_create_should_fail()
    // {
    //     //ARRANGE
    //     var command = new ReserveParkingSpotForVehicle(Guid.Parse("00000000-0000-0000-0000-000000000011"),
    //         Guid.NewGuid(), "John Doe", "abc-123", DateTime.UtcNow.AddDays(1));
    //
    //     //ACT
    //     var reservationId = await _reservationsService.ReserveForVehicleAsync(command);
    //
    //     //ASSERT
    //     reservationId.Should().BeNull();
    // }
    //
    // #region ARRANGE
    //
    // private readonly IClock _clock;
    // private readonly ReservationsService _reservationsService;
    // private readonly IWeeklyParkingSpotRepository _weeklyParkingSpotRepository;
    //
    // public ReservationServiceTests()
    // {
    //     _clock = new TestClock();
    //     _weeklyParkingSpotRepository = new InMemoryWeeklyParkingSpotRepository(_clock);
    //     // var weeklyParkingSpots = new List<WeeklyParkingSpot>
    //     // {
    //     //     new(Guid.Parse("00000000-0000-0000-0000-000000000001"), new Week(_clock.Current()), "P1"),
    //     //     new(Guid.Parse("00000000-0000-0000-0000-000000000002"), new Week(_clock.Current()), "P2"),
    //     //     new(Guid.Parse("00000000-0000-0000-0000-000000000003"), new Week(_clock.Current()), "P3"),
    //     //     new(Guid.Parse("00000000-0000-0000-0000-000000000004"), new Week(_clock.Current()), "P4"),
    //     //     new(Guid.Parse("00000000-0000-0000-0000-000000000005"), new Week(_clock.Current()), "P5")
    //     // };
    //
    //     _reservationsService = new ReservationsService(_clock, _weeklyParkingSpotRepository);
    // }
    //
    // #endregion
}