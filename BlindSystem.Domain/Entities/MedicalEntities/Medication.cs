namespace BlindSystem.Domain.Entities.MedicalEntity
{
    public class Medication : BaseEntity
    {

        public string Name { get; set; }
        public string Dosage { get; set; }

        public List<DateTime> Schedule { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Guid MedicalProfileId { get; set; }
        public MedicalProfile MedicalProfile { get; set; } = null!;
    }
}