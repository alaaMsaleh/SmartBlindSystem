using BlindSystem.Domain.Entities.MedicalEntity;

namespace BlindSystem.Infrastructure.Specification
{
    public class MedicalHistoryByMedicalProfileIdSpec : BaseSpecification<MedicalHistoryEntry>
    {
        public MedicalHistoryByMedicalProfileIdSpec() { }

        // All History Entries for a profile
        public MedicalHistoryByMedicalProfileIdSpec(Guid medicalProfileId)
            : base(h => h.MedicalProfileId == medicalProfileId)
        {
        }
    }
}