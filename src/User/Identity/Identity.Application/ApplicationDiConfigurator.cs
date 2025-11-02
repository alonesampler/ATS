using Identity.Application.Interfaces.UseCases;
using Identity.Application.UseCases;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Application;

public static class ApplicationDiConfigurator
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<IConfirmEmailUseCase, ConfirmEmailUseCase>();
    }
}
