using MySpot.Application.Abstractions;
using MySpot.Core.DomainServices;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;

namespace MySpot.Application.Commands.Handlers;

public sealed class ReserveParkingSpotForCleaningHandler : ICommandHandler<ReserveParkingSpotForCleaning>
{
    private readonly IWeeklyParkingSpotRepository _repository;
    private readonly IParkingReservationService _parkingReservationService;

    public async Task HandleAsync(ReserveParkingSpotForCleaning command)
    {
        var week = new Week(command.Date);
        var weeklyParkingSpot = (await _repository.GetByWeekAsync(week)).ToList();

        _parkingReservationService.ReservationForCleaning(weeklyParkingSpot, new Date(command.Date));

        var tasks = weeklyParkingSpot.Select(x => _repository.UpdateAsync(x));
        await Task.WhenAll(tasks);
    }
}