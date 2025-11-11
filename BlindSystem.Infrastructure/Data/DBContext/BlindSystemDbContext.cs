using BlindSystem.Domain.Entities.DevicesEntities;
using BlindSystem.Domain.Entities.UserEntity;
using BlindSystem.Infrastructure.Configuration.UserModuleConfig;
using Microsoft.EntityFrameworkCore;


namespace BlindSystem.Infrastructure.Data.DBContext
{
    public class BlindSystemDbContext : DbContext
    {
        public BlindSystemDbContext(DbContextOptions<BlindSystemDbContext> options) : base(options)
        {





        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<UserProfile>(new UserProfileConfig());
            modelBuilder.Entity<Device>().HasData(
    new Device
    {
        Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
        DeviceName = "SmartStick",
        BatteryLevel = 80,
        LastSync = DateTime.Parse("2025-11-11T12:00:00"),
        FrimWareVersion = "1.0.0",
        SerialNumber = "SS-0001"
    },
    new Device
    {
        Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
        DeviceName = "SmartGlass",
        BatteryLevel = 60,
        LastSync = DateTime.Parse("2025-11-11T12:00:00"),
        FrimWareVersion = "1.0.0",
        SerialNumber = "SG-0001"
    },
    new Device
    {
        Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
        DeviceName = "Bracelet",
        BatteryLevel = 90,
        LastSync = DateTime.Parse("2025-11-11T12:00:00"),
        FrimWareVersion = "1.0.0",
        SerialNumber = "BR-0001"
    }
);

        }

        public DbSet<UserProfile> UsersProfile { get; set; }
        public DbSet<FaceProfile> FacesProfile { get; set; }
        public DbSet<MedicalProfile> MedicalProfile { get; set; }
        public DbSet<EmergencyContect> EmergencyContect { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<SmartBracelet> SmartBracelets { get; set; }
        public DbSet<SmartGlass> SmartGlass { get; set; }
        public DbSet<SmartStick> SmartStick { get; set; }

    }
}
