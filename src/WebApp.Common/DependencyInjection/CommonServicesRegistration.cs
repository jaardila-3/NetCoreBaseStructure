using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Common.Configurations;

namespace WebApp.Common.DependencyInjection;

public static class CommonServicesRegistration
{
    public static IServiceCollection AddCommonServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Configure feature flags and specific settings
        services.Configure<AdminSettings>(options => configuration.GetSection("AdminSettings").Bind(options));
        services.Configure<FeatureFlags>(options => configuration.GetSection("FeatureFlags").Bind(options));        

        return services;
    }
}