using Identity.Domain.Interfaces;
using Identity.Infrastructure.EfCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure.EfCore;

public static class EfCoreDiConfigurator
{
    internal static void ConfigureEFPostgres(IServiceCollection services, IConfiguration configuration, string connectionString)
    {
        services.AddDbContext<IdentityDbContext>(options =>
        {
            options.UseNpgsql(connectionString,
                x =>
                {
                    x.MigrationsHistoryTable("__EFMigrationsHistory");
                });
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
    }
}
