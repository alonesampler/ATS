using Identity.Application.Interfaces.UseCases;
using Identity.Application.UseCases;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.DI;

public static class ApplicationConfigurator
{
    public static void ConfigureApplication(IServiceCollection services, IConfiguration configuration)
    {
        // Application layer service registrations go here
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
    }
}
