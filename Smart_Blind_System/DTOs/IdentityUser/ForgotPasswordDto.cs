using System.ComponentModel.DataAnnotations;

namespace Smart_Blind_System.API.DTOs.IdentityUser
{
    public class ForgotPasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
