using System.ComponentModel.DataAnnotations;

namespace Smart_Blind_System.API.DTOs.IdentityUser
{
    public class UpdateUserProfileDto
    {

        [StringLength(100, MinimumLength = 2,
            ErrorMessage = "Full name must be between 2 and 100 characters.")]
        public string? FullName { get; set; }

        [StringLength(50, MinimumLength = 2,
            ErrorMessage = "Display name must be between 2 and 50 characters.")]
        public string? DisplayName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [StringLength(150)]
        public string? Email { get; set; }

        [Phone(ErrorMessage = "Invalid phone number format.")]
        [StringLength(20)]
        public string? PhoneNumber { get; set; }
        [StringLength(500)]
        public string? UserImage { get; set; }


        [StringLength(20)]
        public string? Gender { get; set; }

        public DateTime? BirthDate { get; set; }
    }
}
