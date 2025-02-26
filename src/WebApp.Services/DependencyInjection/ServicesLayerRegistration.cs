using Microsoft.Extensions.DependencyInjection;
using WebApp.Common.Interfaces;
using WebApp.Services.Implementations;

namespace WebApp.Services.DependencyInjection;

public static class ServicesLayerRegistration
{
    public static IServiceCollection AddServicesLayerServices(this IServiceCollection services)
    {
        //configure DI for services
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        
        return services;
    }
}