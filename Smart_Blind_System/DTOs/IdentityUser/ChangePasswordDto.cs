using System.ComponentModel.DataAnnotations;

namespace Smart_Blind_System.API.DTOs.IdentityUser
{
    public class ChangePasswordDto
    {
        [Required(ErrorMessage = "Current password is required.")]
        public string CurrentPassword { get; set; } = null!;

        [Required(ErrorMessage = "New password is required.")]
        [StringLength(20, MinimumLength = 8,
            ErrorMessage = "Password must be between 8 and 20 characters.")]
        [RegularExpression(
            @"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]+$",
            ErrorMessage = "Password must contain at least one uppercase letter, one digit, and one special character (@$!%*?&).")]
        public string NewPassword { get; set; } = null!;

        [Required(ErrorMessage = "Confirm password is required.")]
        [Compare(nameof(NewPassword), ErrorMessage = "New password and confirmation do not match.")]
        public string ConfirmNewPassword { get; set; } = null!;
    }
}
