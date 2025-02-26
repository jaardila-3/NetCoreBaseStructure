using Microsoft.EntityFrameworkCore;
using WebApp.Business.BusinessServices.LoggerException;
using WebApp.Business.Interfaces;
using WebApp.Common.Configurations;
using WebApp.Common.Interfaces;
using WebApp.Data.Context;
using WebApp.Data.Initialization;
using WebApp.Data.Interfaces;
using WebApp.Data.Repositories;
using WebApp.Services.Implementations;

namespace WebApp.Web.DependencyInjection;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        //configure DI for context and connection string with oracle db
        services.AddDbContext<WebAppDbContext>(options =>
            options.UseOracle(configuration.GetConnectionString("DefaultConnection")));

        //configure DI for repositories and UnitOfWork (Data Layer)
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<DatabaseInitializer>();

        //configure DI for LoggerException (Business Layer)
        services.AddScoped<ILoggerException, OracleLoggerException>();

        //configure DI for services (Services Layer)
        services.AddScoped<IPasswordHasher, PasswordHasher>();

        // Configure feature flags and specific settings
        services.Configure<AdminSettings>(configuration.GetSection("AdminSettings"));
        services.Configure<FeatureFlags>(configuration.GetSection("FeatureFlags"));

        return services;
    }
}