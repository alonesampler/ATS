using Identity.Infrastructure.Brokers;
using Identity.Infrastructure.EfCore;
using Identity.Infrastructure.Hasher;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure;

public static class InfrastructureDiConfiguration
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection("ConnectionStrings");
        var postgres = section["PostgresSql"];
        var kafka = section["Kafka"];

        // Configure Password Hasher
        PasswordHasherDiConfigurator.ConfigurePasswordHasher(services, configuration);
        KafkaDiConfigurator.ConfigureKafka(services, configuration, kafka);
        EfCoreDiConfigurator.ConfigureEFPostgres(services, configuration, postgres);
    }
}
