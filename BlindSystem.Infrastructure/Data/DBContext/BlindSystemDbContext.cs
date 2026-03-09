using BlindSystem.Domain.Entities;
using BlindSystem.Domain.Entities.ActionEntity;
using BlindSystem.Domain.Entities.DevicesEntities;
using BlindSystem.Domain.Entities.MedicalEntities;
using BlindSystem.Domain.Entities.MedicalEntity;
using BlindSystem.Domain.Entities.UserEntity;
using BlindSystem.Infrastructure.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace BlindSystem.Infrastructure.Data.DBContext
{
    public class BlindSystemDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public BlindSystemDbContext(DbContextOptions<BlindSystemDbContext> options) : base(options)
        {





        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ApplicatopnUserConfiguration());
            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
            modelBuilder.Entity<IdentityRole<Guid>>().ToTable("Roles");
            //to Make GeoLocation Owned
            modelBuilder.Entity<Alert>().OwnsOne(e => e.Location);




            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.NoAction;
            }
        }
        public DbSet<FaceProfile> FacesProfile { get; set; }
        public DbSet<MedicalProfile> MedicalProfile { get; set; }
        public DbSet<EmergencyContect> EmergencyContect { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<SmartBracelet> SmartBracelets { get; set; }
        public DbSet<SmartGlass> SmartGlass { get; set; }
        public DbSet<SmartStick> SmartStick { get; set; }
        public DbSet<Alert> Alerts { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<MedicalProfile> MedicalProfiles { get; set; }
        public DbSet<Allergy> Allergies { get; set; }
        public DbSet<ChronicDisease> ChronicDiseases { get; set; }
        public DbSet<MedicalHistoryEntry> MedicalHistoryEntries { get; set; }


    }
}
