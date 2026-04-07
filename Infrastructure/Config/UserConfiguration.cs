using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // table name
        builder.ToTable("Users");

        // primary key
        builder.HasKey(u => u.Id);

        // prop
        builder.Property(u => u.Name).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Role).IsRequired().HasMaxLength(50);
        builder.Property(u => u.Location).IsRequired().HasMaxLength(100);

        //  one user can have many devices
        builder.HasMany(u => u.Devices)
            .WithOne(d => d.User)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
