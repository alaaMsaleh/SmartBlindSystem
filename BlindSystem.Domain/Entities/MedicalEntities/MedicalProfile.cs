using BlindSystem.Domain.Entities.Enums;
using BlindSystem.Domain.Entities.MedicalEntities;

namespace BlindSystem.Domain.Entities.MedicalEntity
{
    public class MedicalProfile : BaseEntity
    {
        public MedicalProfile() { }

        public BloodType BloodType { get; set; }
        public float? Height { get; set; }
        public float? Weight { get; set; }
        public string Notes { get; set; } = string.Empty;
        public string DrPhone { get; set; } = string.Empty;

        public Gender Gender { get; set; }


        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }

        public ICollection<Medication> Medications { get; set; } = new List<Medication>();
        public ICollection<Allergy> Allergies { get; set; } = new List<Allergy>();
        public ICollection<ChronicDisease> ChronicDiseases { get; set; } = new List<ChronicDisease>();
        public ICollection<MedicalHistoryEntry> HistoryEntries { get; set; } = new List<MedicalHistoryEntry>();
    }
}
