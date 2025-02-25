using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.Data.Models.Entities;

namespace WebApp.Data.Models.Configurations;

public class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        // Primary key
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasColumnType("CHAR(26)")
            .ValueGeneratedNever();

        builder.Property(e => e.CreatedAt)
            .HasColumnType("TIMESTAMP")
            .IsRequired();

        builder.Property(e => e.CreatedBy)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(e => e.UpdatedAt)
            .HasColumnType("TIMESTAMP");

        builder.Property(e => e.UpdatedBy)
            .HasMaxLength(50);

        builder.Property(e => e.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        // Indexes
        builder.HasIndex(e => e.CreatedAt);

        // Filter out deleted entities
        builder.HasQueryFilter(e => !e.IsDeleted);
    }
}