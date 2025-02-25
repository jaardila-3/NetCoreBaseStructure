using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApp.Data.Models.Entities;

namespace WebApp.Data.Context;

public class WebAppDbContext(DbContextOptions<WebAppDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WebAppDbContext).Assembly);

        // Converter for Ulid
        var ulidConverter = new ValueConverter<Ulid, string>(
            v => v.ToString(),
            v => Ulid.Parse(v));

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var properties = entityType.ClrType.GetProperties()
                .Where(p => p.PropertyType == typeof(Ulid));

            foreach (var property in properties)
            {
                modelBuilder.Entity(entityType.Name)
                    .Property(property.Name)
                    .HasConversion(ulidConverter);
            }
        }

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Log> Logs { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
}
