using BlindSystem.Domain.Entities.MedicalEntity;

namespace BlindSystem.Domain.Entities.MedicalEntities
{
    public class ChronicDisease : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? DiagnosedDate { get; set; }

        public Guid MedicalProfileId { get; set; }
        public MedicalProfile MedicalProfile { get; set; } = null!;
    }
}
