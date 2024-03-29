﻿using Microsoft.AspNetCore.Mvc;
using MySpot.Application.Abstractions;
using MySpot.Application.Commands;
using MySpot.Application.DTO;
using MySpot.Application.Queries;

namespace MySpot.Api.Controllers;

[ApiController]
[Route("parking-spot")]
public class ParkingSpotController : ControllerBase
{
    private readonly ICommandHandler<ReserveParkingSpotForVehicle> _reserveParkingSpotForVehicleHandler;
    private readonly ICommandHandler<ReserveParkingSpotForCleaning> _reserveParkingSpotForCleaningHandler;
    private readonly IQueryHandler<GetWeeklyParkingSpots, IEnumerable<WeeklyParkingSpotDto>> _getWeeklyParkingSpotsHandler;

    public ParkingSpotController(
        ICommandHandler<ReserveParkingSpotForVehicle> reserveParkingSpotForVehicleHandler,
        ICommandHandler<ReserveParkingSpotForCleaning> reserveParkingSpotForCleaningHandler,
        IQueryHandler<GetWeeklyParkingSpots, IEnumerable<WeeklyParkingSpotDto>> getWeeklyParkingSpotsHandler)
    {
        _reserveParkingSpotForVehicleHandler = reserveParkingSpotForVehicleHandler;
        _reserveParkingSpotForCleaningHandler = reserveParkingSpotForCleaningHandler;
        _getWeeklyParkingSpotsHandler = getWeeklyParkingSpotsHandler;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<WeeklyParkingSpotDto>>> Get([FromQuery] GetWeeklyParkingSpots query)
        => Ok(await _getWeeklyParkingSpotsHandler.HandleAsync(query));

    [HttpPost("{parkingSpotId:guid}/reservations/vehicle")]
    public async Task<ActionResult> Post(Guid parkingSpotId, ReserveParkingSpotForVehicle command)
    {
        await _reserveParkingSpotForVehicleHandler.HandleAsync(command with { ParkingSpotId = parkingSpotId });
        return NoContent();
    }

    [HttpPost("reservations/vehicle")]
    public async Task<ActionResult> Post(ReserveParkingSpotForCleaning command)
    {
        await _reserveParkingSpotForCleaningHandler.HandleAsync(command);
        return NoContent();
    }
}