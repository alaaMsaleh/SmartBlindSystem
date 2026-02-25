using BlindSystem.Domain.Entities;
using BlindSystem.Domain.Entities.DevicesEntities;
using BlindSystem.Domain.Entities.UserEntity;
using BlindSystem.Infrastructure.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace BlindSystem.Infrastructure.Data.DBContext
{
    public class BlindSystemDbContext : IdentityDbContext<ApplicationUser>
    {
        public BlindSystemDbContext(DbContextOptions<BlindSystemDbContext> options) : base(options)
        {





        }

        protected BlindSystemDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ApplicatopnUserConfiguration());

        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<FaceProfile> FacesProfile { get; set; }
        public DbSet<MedicalProfile> MedicalProfile { get; set; }
        public DbSet<EmergencyContect> EmergencyContect { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<SmartBracelet> SmartBracelets { get; set; }
        public DbSet<SmartGlass> SmartGlass { get; set; }
        public DbSet<SmartStick> SmartStick { get; set; }

    }
}
