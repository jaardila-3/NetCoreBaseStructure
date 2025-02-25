using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.Data.Models.Entities;

namespace WebApp.Data.Models.Configurations;

public class UserRoleConfiguration : BaseEntityConfiguration<UserRole>
{
    public override void Configure(EntityTypeBuilder<UserRole> builder)
    {
        base.Configure(builder);

        builder.ToTable("USER_ROLES");

        builder.Property(ur => ur.UserId)
            .HasColumnType("CHAR(26)")
            .IsRequired();

        builder.Property(ur => ur.RoleId)
            .HasColumnType("CHAR(26)")
            .IsRequired();

        // Composite index to avoid duplicates of  UserId-RoleId
        builder.HasIndex(ur => new { ur.UserId, ur.RoleId }).IsUnique();
    }
}