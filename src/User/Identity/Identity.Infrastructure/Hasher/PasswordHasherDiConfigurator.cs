using Identity.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure.Hasher;

public static class PasswordHasherDiConfigurator
{
    internal static void ConfigurePasswordHasher(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
    }
}
