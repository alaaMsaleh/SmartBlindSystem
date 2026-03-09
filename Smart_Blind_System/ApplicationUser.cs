using BlindSystem.Domain.Entities.MedicalEntity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace BlindSystem.Domain.Entities.UserEntity
{
    // Ensure ApplicationUser inherits from IdentityUser<Guid>
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FullName { get; set; }
        public string DisplayName { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public string? UserImage { get; set; }
        public bool IsEmergencyMode { get; set; }
        public double? LastLatitude { get; set; }
        public double? LastLongitude { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeviceActive { get; set; }
        public virtual MedicalProfile MedicalProfile { get; set; }
        public virtual ICollection<EmergencyContect> EmergencyContects { get; set; }
        public virtual ICollection<FaceProfile> faceProfiles { get; set; }
    }
}