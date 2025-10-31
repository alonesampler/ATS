using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.DI;

public static class DiConfigurator
{
    public static void ConfigureDi(this IServiceCollection services, IConfiguration configuration)
    {
        ApplicationConfigurator.Configure(services, configuration);
        InfrastructureConfigurator.Configure(services, configuration);
    }
}
