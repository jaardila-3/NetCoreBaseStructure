using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApp.Data.Context;

namespace WebApp.Data.Initialization;

public class DatabaseMigrator(WebAppDbContext context, ILogger<DatabaseMigrator> logger)
{
    private readonly WebAppDbContext _context = context;
    private readonly ILogger<DatabaseMigrator> _logger = logger;

    public async Task MigrateDatabaseAsync()
    {
        _logger.LogInformation("Starting database migration...");

        try
        {
            await _context.Database.MigrateAsync();
            _logger.LogInformation("Database migration completed.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred during database migration.");
            throw;
        }
    }
}