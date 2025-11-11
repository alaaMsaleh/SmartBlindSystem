using BlindSystem.Domain.Entities.UserEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlindSystem.Infrastructure.Configuration.UserModuleConfig
{
    public class UserProfileConfig : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            //at Attributes
            builder.HasKey(u => u.Id);

            builder.Property(u => u.FullName)
                .HasMaxLength(100)
                .IsRequired();

            //Relation  at Entity


            builder.HasOne(u => u.MedicalProfile)
                .WithOne(m => m.UserProfile)
                .HasForeignKey<MedicalProfile>(m => m.UserId);

            builder.HasMany(u => u.EmergencyContects)
                .WithOne(e => e.user)
                .HasForeignKey(e => e.UserId);


            builder.HasMany(u => u.faceProfiles)
             .WithOne(e => e.userProfile)
             .HasForeignKey(e => e.UserId);



        }
    }
}
