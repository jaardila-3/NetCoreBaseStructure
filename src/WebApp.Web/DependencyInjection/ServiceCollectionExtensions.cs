using WebApp.Business.DependencyInjection;
using WebApp.Common.DependencyInjection;
using WebApp.Services.DependencyInjection;

namespace WebApp.Web.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Register services from other layers
        services
            .AddBusinessLayerServices(configuration)
            .AddServicesLayerServices()
            .AddCommonServices(configuration);
        
        return services;
    }
}