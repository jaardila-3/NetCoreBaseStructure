using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApp.Data.Models.Entities;

namespace WebApp.Data.Models.Configurations;

public class LogEntityConfiguration : IEntityTypeConfiguration<Log>
{
    public void Configure(EntityTypeBuilder<Log> builder)
    {
        builder.ToTable("LOGS");

        // primary key
        builder.HasKey(l => l.Id);
        builder.Property(l => l.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder.Property(l => l.Timestamp)
            .IsRequired();

        builder.Property(l => l.Level)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(l => l.Message)
            .IsRequired()
            .HasColumnType("NCLOB");

        builder.Property(l => l.Exception)
            .HasColumnType("NCLOB");

        builder.Property(l => l.Properties)
            .HasColumnType("NCLOB");

        // index for Timestamp
        builder.HasIndex(l => l.Timestamp);
    }
}