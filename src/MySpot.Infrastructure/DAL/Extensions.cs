using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MySpot.Core.Repositories;
using MySpot.Infrastructure.DAL.Repository;

namespace MySpot.Infrastructure.DAL;

internal static class Extensions
{
    public static IServiceCollection AddPostgres(this IServiceCollection service)
    {
        const string connectionString = "Host=localhost;Port=5432;Database=MySpot;Username=postgres;Password=postgres";
        service.AddDbContext<MySpotDbContext>(x => x.UseNpgsql(connectionString));
        service.AddScoped<IWeeklyParkingSpotRepository, PostgresWeeklyParkingSpotRepository>();
        service.AddHostedService<DatabaseInitializer>();

        // EF Core + Npgsgl issue
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        return service;
    }
}