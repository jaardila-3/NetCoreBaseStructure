using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.Data.Models.Entities;

namespace WebApp.Data.Models.Configurations;

public class UserConfiguration : BaseEntityConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        // apply base configuration
        base.Configure(builder);

        builder.ToTable("USERS");

        builder.Property(u => u.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(u => u.LastName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(u => u.IdentificationNumber)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(u => u.Username)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(u => u.Dependency)
            .HasMaxLength(100);

        builder.Property(u => u.Email)
            .HasMaxLength(255);

        builder.Property(u => u.PasswordHash)
            .HasMaxLength(255);

        builder.Property(u => u.LockoutEnd)
            .HasColumnType("TIMESTAMP");

        builder.Property(u => u.LockoutEnabled)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(u => u.AccessFailedCount)
            .IsRequired()
            .HasDefaultValue(0);

        // Indexes
        builder.HasIndex(u => u.IdentificationNumber).IsUnique();
        builder.HasIndex(u => u.Username).IsUnique();
        builder.HasIndex(u => u.Email).IsUnique();

        // Relationship with UserRole
        builder.HasMany(u => u.UserRoles)
            .WithOne(ur => ur.User)
            .HasForeignKey(ur => ur.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}