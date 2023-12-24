using Microsoft.AspNetCore.Mvc;
using MySpot.Api.Commands;
using MySpot.Api.DTO;
using MySpot.Api.Entities;
using MySpot.Api.Services;
using MySpot.Api.ValueObjects;

namespace MySpot.Api.Controllers;

[ApiController]
[Route("reservations")]
public class ReservationsController : ControllerBase
{
    private readonly IReservationsService _reservationsService;

    public ReservationsController(IReservationsService reservationsService)
    {
        _reservationsService = reservationsService;
    }

    [HttpGet]
    public ActionResult GetAll()
        => Ok(_reservationsService.GetAllWeekly());

    [HttpGet("{id:guid}")]
    public ActionResult<ReservationDto> Get(Guid id)
    {
        var reservation = _reservationsService.Get(id);
        if (reservation is null)
        {
            return NotFound();
        }

        return reservation;
    }

    [HttpPost]
    public ActionResult Post([FromBody] CreateReservation command)
    {
        var id = _reservationsService.Create(command with { ReservationId = Guid.NewGuid() });

        if (id is null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(Get), new { Id = id }, default);
    }

    [HttpPut("{id:guid}")]
    public ActionResult Put(Guid id, ChangeReservationLicencePlate command)
    {
        var succeeded = _reservationsService.Update(command with { ReservationId = id });
        if (!succeeded)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public ActionResult Delete(Guid id)
    {
        var succeeded = _reservationsService.Delete(new DeleteReservation(id));
        if (!succeeded)
        {
            return NotFound();
        }

        return NoContent();
    }
}