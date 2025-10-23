using Identity.Application;
using Identity.Application.Interfaces.Producer;
using Identity.Infrastructure.Brokers;
using Identity.Infrastructure.ConnectionStrings;
using Identity.Infrastructure.EfCore;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.DI;

public static class InfrastructureConfigurator
{
    private static void ConfigureEFPostgres(IServiceCollection services, IConfiguration configuration, ConnectionStrings connectionStrings)
    {
        services.AddDbContext<IdentityDbContext>(options =>
        {
            options.UseNpgsql(connectionStrings.PostgresSql,
                x =>
                {
                    x.MigrationsHistoryTable("__EFMigrationsHistory");
                });
        });
    }

    private static void ConfigureKafka(IServiceCollection services, IConfiguration configuration, ConnectionStrings connectionStrings)
    {
        services.AddMassTransit(x =>
        {
            x.UsingInMemory();

            x.AddRider(rider =>
            {
                rider.AddProducer<CreateUserMessage>("user-created");

                rider.UsingKafka((context, k) =>
                {
                    k.Host(connectionStrings.Kafka);
                });
            });
        });

        services.AddScoped(typeof(IProducer<>), typeof(KafkaProducer<>));
    }


}
