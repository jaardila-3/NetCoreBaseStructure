using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Business.BusinessServices.LoggerException;
using WebApp.Business.Interfaces;
using WebApp.Data.DependencyInjection;

namespace WebApp.Business.DependencyInjection;

public static class BusinessLayerRegistration
{
    public static IServiceCollection AddBusinessLayerServices(this IServiceCollection services, IConfiguration configuration)
    {
        //configure DI for services
        services.AddScoped<ILoggerException, OracleLoggerException>();

        // Register services from Data layer
        services
            .AddDataLayerServices(configuration);

        return services;
    }
}