using Identity.Application.Interfaces.UseCases;
using Identity.Application.UseCases;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.DI;

public static class ApplicationConfigurator
{
    public static void Configure(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
    }
}
