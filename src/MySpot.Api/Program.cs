using MySpot.Application;
using MySpot.Application.Abstractions;
using MySpot.Application.DTO;
using MySpot.Application.Queries;
using MySpot.Core;
using MySpot.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddCore()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Host.UseSerilog(((context, configuration) =>
{
    configuration.WriteTo
        .Console()
        .WriteTo
        .File("logs.txt")
        .WriteTo
        .Seq("http://localhost:5341");
}));

var app = builder.Build();

app.UseInfrastructure();
app.MapGet("api", (HttpContext context) => { return Results.Ok("Hello World"); });
app.MapGet("api/users/{userId:guid}", async (Guid userId, IQueryHandler<GetUser, UserDto> handler) =>
{
    var userDto = await handler.HandleAsync(new GetUser { UserId = userId });
    return userDto is null ? Results.NotFound() : Results.Ok(userDto);
});

app.Run();