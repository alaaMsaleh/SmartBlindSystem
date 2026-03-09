using BlindSystem.Domain.Entities.MedicalEntity;

namespace BlindSystem.Domain.Entities.MedicalEntities
{
    public class Allergy : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Severity { get; set; }        // Mild / Moderate / Severe
        public string? Reaction { get; set; }

        public Guid MedicalProfileId { get; set; }
        public MedicalProfile MedicalProfile { get; set; } = null!;
    }

}
