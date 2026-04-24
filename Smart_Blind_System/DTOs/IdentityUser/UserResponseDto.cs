namespace Smart_Blind_System.API.DTOs.IdentityUser
{
    public class UserResponseDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? UserImage { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
