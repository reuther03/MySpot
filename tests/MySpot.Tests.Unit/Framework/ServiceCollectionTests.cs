using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace MySpot.Tests.Unit.Framework;

public class ServiceCollectionTests
{
    [Fact]
    public void test()
    {
        var serviceCollection = new ServiceCollection();
        // serviceCollection.AddSingleton<IMessenger, Messenger>();
        // serviceCollection.AddTransient<IMessenger, Messenger>();
        serviceCollection.AddScoped<IMessenger, Messenger>();

        var serviceProvider = serviceCollection.BuildServiceProvider();
        using (var scope = serviceProvider.CreateScope())
        {
            var messenger1 = scope.ServiceProvider.GetRequiredService<IMessenger>();
            var messenger2 = scope.ServiceProvider.GetRequiredService<IMessenger>();
            messenger1.Should().Be(messenger2);
        }

        using (var scope2 = serviceProvider.CreateScope())
        {
            var messenger1 = scope2.ServiceProvider.GetRequiredService<IMessenger>();
            var messenger2 = scope2.ServiceProvider.GetRequiredService<IMessenger>();
            messenger1.Should().Be(messenger2);
        }
    }

    private interface IMessenger
    {
        void Send();
    }

    private class Messenger : IMessenger
    {
        private readonly Guid _id = Guid.NewGuid();

        public void Send()
        {
            Console.WriteLine($"Sending a message... [ID {_id}]");
        }
    }

}