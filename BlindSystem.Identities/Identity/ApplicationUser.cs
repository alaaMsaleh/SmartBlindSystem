using BlindSystem.Domain.Entities.UserEntity;
using Microsoft.AspNetCore.Identity;

namespace BlindSystem.Identities.Identity
{
    //non Generic 
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
        public UserProfile user { get; set; }
    }
}
