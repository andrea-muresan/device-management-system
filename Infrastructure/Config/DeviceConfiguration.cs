using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config;

public class DeviceConfiguration : IEntityTypeConfiguration<Device>
{
    public void Configure(EntityTypeBuilder<Device> builder)
    {
        builder.ToTable("devices");

        builder.HasKey(d => d.Id);
        builder.Property(d => d.Name).IsRequired().HasMaxLength(100);
        builder.Property(d => d.Manufacturer).IsRequired().HasMaxLength(50);
        builder.Property(d => d.Type).IsRequired().HasConversion<string>();
        builder.Property(d => d.OS).IsRequired().HasMaxLength(30);
        builder.Property(d => d.OSVersion).IsRequired().HasMaxLength(20);
        builder.Property(d => d.Processor).IsRequired().HasMaxLength(100);
        builder.Property(d => d.RAM).IsRequired();
        builder.Property(d => d.Description).HasMaxLength(500).IsRequired();

        builder.HasOne(d => d.User)
               .WithMany()
               .HasForeignKey(d => d.UserId)
               .OnDelete(DeleteBehavior.SetNull);
    }
}
