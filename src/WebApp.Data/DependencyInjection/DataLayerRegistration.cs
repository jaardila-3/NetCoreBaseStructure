using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Data.Context;
using WebApp.Data.Initialization;
using WebApp.Data.Interfaces;
using WebApp.Data.Repositories;

namespace WebApp.Data.DependencyInjection;

public static class DataLayerRegistration
{
    public static IServiceCollection AddDataLayerServices(this IServiceCollection services, IConfiguration configuration)
    {
        //configure DI for context and connection string with oracle db
        services.AddDbContext<WebAppDbContext>(options =>
            options.UseOracle(configuration.GetConnectionString("DefaultConnection")));

        //configure DI for repositories and UnitOfWork
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<DatabaseMigrator>();
        services.AddScoped<DatabaseInitializer>();

        return services;
    }
}