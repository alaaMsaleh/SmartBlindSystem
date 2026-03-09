using BlindSystem.Domain.Entities.ActionEntity;
using BlindSystem.Domain.Entities.MedicalEntity;
using BlindSystem.Domain.Entities.UserEntity;
using Microsoft.AspNetCore.Identity;

namespace BlindSystem.Domain.Entities
{
    //non Generic 
    public class ApplicationUser : IdentityUser<Guid>
    {
        //User Base information
        public string FullName { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public string? RefreshToken { get; set; } = null!;

        public DateTime BirthDate { get; set; }
        public string? Gender { get; set; } = null!;
        public string? UserImage { get; set; }


        public bool IsEmergencyMode { get; set; } = false;
        public double? LastLatitude { get; set; }
        public double? LastLongitude { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsDeviceActive { get; set; } = true;



        public virtual MedicalProfile MedicalProfile { get; set; }


        public virtual ICollection<EmergencyContect> EmergencyContects { get; set; } = new List<EmergencyContect>();

        public virtual ICollection<FaceProfile> faceProfiles { get; set; } = new List<FaceProfile>();

        public virtual ICollection<Alert> Alerts { get; set; } = new List<Alert>();
        public virtual ICollection<Medication> Medications { get; set; } = new List<Medication>();

    }
}
