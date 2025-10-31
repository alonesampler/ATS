using Identity.Application;
using Identity.Application.Interfaces.Producer;
using Identity.Domain.Interfaces;
using Identity.Infrastructure.Brokers;
using Identity.Infrastructure.ConnectionStrings;
using Identity.Infrastructure.EfCore;
using Identity.Infrastructure.Hasher;
using Identity.Infrastructure.Implementations.Repositories;
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

    private static void ConfigurePasswordHasher(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
    }
}
