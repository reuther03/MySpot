using Microsoft.Extensions.DependencyInjection;
using MySpot.Application.Abstractions;


namespace MySpot.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection service)
    {
        var applicationAssembly = typeof(ICommandHandler<>).Assembly;

        service.Scan(s => s.FromAssemblies(applicationAssembly)
            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return service;
    }
}