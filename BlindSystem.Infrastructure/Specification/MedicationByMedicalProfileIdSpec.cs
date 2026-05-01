using BlindSystem.Domain.Entities.MedicalEntity;

namespace BlindSystem.Infrastructure.Specification
{
    public class MedicationByMedicalProfileIdSpec : BaseSpecification<Medication>
    {
        public MedicationByMedicalProfileIdSpec() { }

        // All Medications for a profile
        public MedicationByMedicalProfileIdSpec(Guid medicalProfileId)
            : base(m => m.MedicalProfileId == medicalProfileId)
        {
        }
    }
}

