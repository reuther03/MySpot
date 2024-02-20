using Microsoft.Extensions.DependencyInjection;
using MySpot.Core.DomainServices;
using MySpot.Core.Policies;

namespace MySpot.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection service)
    {
        service.AddSingleton<IReservationPolicy, RegularEmployeeReservationPolicy>();
        service.AddSingleton<IReservationPolicy, ManagerReservationPolicy>();
        service.AddSingleton<IReservationPolicy, BossReservationPolicy>();
        service.AddSingleton<IParkingReservationService, ParkingReservationService>();


        return service;
    }
}