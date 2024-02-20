using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySpot.Application.Abstractions;
using MySpot.Core.Repositories;
using MySpot.Infrastructure.DAL.Decorators;
using MySpot.Infrastructure.DAL.Repository;

namespace MySpot.Infrastructure.DAL;

internal static class Extensions
{
    private const string OptionsSectionName = "postgres";

    public static IServiceCollection AddPostgres(this IServiceCollection service, IConfiguration configuration)
    {
        service.Configure<PostgresOptions>(configuration.GetRequiredSection(OptionsSectionName));
        var postgresOptions = configuration.GetOptions<PostgresOptions>(OptionsSectionName);
        service.AddDbContext<MySpotDbContext>(x => x.UseNpgsql(postgresOptions.ConnectionString));
        service.AddScoped<IWeeklyParkingSpotRepository, PostgresWeeklyParkingSpotRepository>();
        service.AddScoped<IUserRepository, UserRepository>();
        service.AddHostedService<DatabaseInitializer>();
        service.AddScoped<IUnitOfWork, PostgresUnitOfWork>();

        service.TryDecorate(typeof(ICommandHandler<>), typeof(UnitOfWorkCommandHandlerDecorator<>));

        // EF Core + Npgsgl issue
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        return service;
    }
}