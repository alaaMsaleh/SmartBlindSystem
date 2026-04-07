namespace BlindSystem.Domain.Entities.MedicalEntity
{
    public class MedicalHistoryEntry : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }
        public string? DoctorName { get; set; }

        public Guid MedicalProfileId { get; set; }
        public MedicalProfile MedicalProfile { get; set; } = null!;
    }
}

