using Microsoft.EntityFrameworkCore;
using WebApp.Business.BusinessServices.LoggerException;
using WebApp.Business.Interfaces;
using WebApp.Data.Context;
using WebApp.Data.Interfaces;
using WebApp.Data.Repositories;

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

        //configure DI for LoggerException (Business Layer)
        services.AddScoped<ILoggerException, OracleLoggerException>();

        return services;
    }
}