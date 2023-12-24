using MySpot.Api.Entities;
using MySpot.Api.Repository;
using MySpot.Api.Services;
using MySpot.Api.ValueObjects;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddSingleton<IWeeklyParkingSpotRepository, InMemoryWeeklyParkingSpotRepository>()
    .AddSingleton<IReservationsService, ReservationsService>()
    .AddSingleton<IClock, Clock>()
    .AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();