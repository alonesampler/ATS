using Identity.Application.Interfaces.Producer;
using Identity.Domain.Events;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure.Brokers;

internal class KafkaDiConfigurator
{
    public static void ConfigureKafka(IServiceCollection services, IConfiguration configuration, string connectionString)
    {
        services.AddMassTransit(x =>
        {
            x.UsingInMemory((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
            });

            x.AddRider(rider =>
            {
                rider.AddProducer<UserRegisteredEvent>("user-created");
                rider.AddProducer<EmailConfirmedEvent>("email-confirmed");

                rider.UsingKafka((context, k) =>
                {
                    k.Host(connectionString);
                });
            });
        });

        services.AddScoped(typeof(IProducer<>), typeof(KafkaProducer<>));
    }
}
