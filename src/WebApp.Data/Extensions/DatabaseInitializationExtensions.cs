using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using WebApp.Common.Configurations;
using WebApp.Data.Initialization;

namespace WebApp.Data.Extensions;

public static class DatabaseInitializationExtensions
{
    public static async Task InitializeDatabaseAsync(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var migrator = scope.ServiceProvider.GetRequiredService<DatabaseMigrator>();
        var initializer = scope.ServiceProvider.GetRequiredService<DatabaseInitializer>();
        var featureFlags = scope.ServiceProvider.GetRequiredService<IOptions<FeatureFlags>>().Value;

        // Check if the database should be migrated at startup via feature flag
        if (featureFlags.MigrateAtStartup)
        {
            await migrator.MigrateDatabaseAsync();
        }

        // Seed roles and admin user
        await initializer.SeedAsync();

    }
}