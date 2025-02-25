using Microsoft.EntityFrameworkCore;
using WebApp.Data.Models.Configurations;
using WebApp.Data.Models.Entities;

namespace WebApp.Data.Context;

public class WebAppDbContext(DbContextOptions<WebAppDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LogEntityConfiguration).Assembly);
    }
    public DbSet<Log> Logs { get; set; }
}
