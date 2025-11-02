using Identity.Application.Interfaces.Producer;
using Identity.Domain.Events;
using Identity.Domain.Interfaces;
using Identity.Infrastructure.Brokers;
using Identity.Infrastructure.ConnectionStrings;
using Identity.Infrastructure.ConStrings;
using Identity.Infrastructure.EfCore;
using Identity.Infrastructure.EfCore.Repositories;
using Identity.Infrastructure.Hasher;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.DI;

public static class InfrastructureConfigurator
{
    public static void Configure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetSection(nameof(ConnectionStrings))
            .Get<ConnectionStrings>();

        ConfigureEFPostgres(services, configuration, connectionString);
        ConfigureKafka(services, configuration, connectionString);
        ConfigurePasswordHasher(services, configuration);
    }

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

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
    }

    private static void ConfigureKafka(IServiceCollection services, IConfiguration configuration, ConnectionStrings connectionStrings)
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
                    k.Host(connectionStrings.Kafka);
                });
            });
        });

        services.AddScoped(typeof(IProducer<>), typeof(KafkaProducer<>));
    }

    private static void ConfigurePasswordHasher(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
    }
}
