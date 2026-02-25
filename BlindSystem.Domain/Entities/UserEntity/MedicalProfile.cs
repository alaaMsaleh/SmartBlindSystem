namespace BlindSystem.Domain.Entities.UserEntity
{
    public class MedicalProfile
    {
        public Guid Id { get; set; }

        public string BoodType { get; set; } = string.Empty;
        public string Allergies { get; set; } = string.Empty;

        public string Medications { get; set; } = string.Empty;

        public string Notes { get; set; } = string.Empty;

        public string DrPhone { get; set; } = string.Empty;

        public string UserId { get; set; }
        public ApplicationUser User { get; set; } //Navigation Property
    }
}
