using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Business.BusinessServices.LoggerException;
using WebApp.Business.Interfaces;
using WebApp.Data.Context;
using WebApp.Data.Interfaces;
using WebApp.Data.Repositories;

namespace WebApp.IntegrationTests.Fixtures;

public class DatabaseFixture : IDisposable
{
    public IServiceProvider ServiceProvider { get; private set; }
    public WebAppDbContext DbContext { get; private set; }

    public DatabaseFixture()
    {
        // configure services
        var services = new ServiceCollection();

        // Register DbContext (Data Layer)
        services.AddDbContext<WebAppDbContext>(options => options.UseInMemoryDatabase("TestDb"));

        // Register Repository and UnitOfWork (Data Layer)
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Register LoggerException (Business Layer)
        services.AddScoped<ILoggerException, OracleLoggerException>();

        // Build the service provider
        ServiceProvider = services.BuildServiceProvider();

        // create the database
        DbContext = ServiceProvider.GetRequiredService<WebAppDbContext>();
        DbContext.Database.EnsureCreated();
    }

    // Reset the database before each test to isolate the state
    public void ResetDatabase()
    {
        DbContext.Database.EnsureDeleted();
        DbContext.Database.EnsureCreated();
    }

    public void Dispose() => DbContext.Dispose();
}