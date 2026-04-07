using Core.Entities;
using Infrastructure.Config;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DeviceDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Device> Devices { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfiguration(new DeviceConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}
