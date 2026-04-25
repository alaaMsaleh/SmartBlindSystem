using BlindSystem.Domain.Entities.MedicalEntities;

namespace BlindSystem.Infrastructure.Specification
{
    public class AllergyByMedicalProfileIdSpec : BaseSpecification<Allergy>
    {
        public AllergyByMedicalProfileIdSpec() { }
        // All Allergies
        public AllergyByMedicalProfileIdSpec(Guid medicalProfileId)
            : base(a => a.MedicalProfileId == medicalProfileId)
        {
        }

        // Filtered by Severity
        public AllergyByMedicalProfileIdSpec(Guid medicalProfileId, string severity)
            : base(a => a.MedicalProfileId == medicalProfileId
                     && a.Severity == severity)
        {
        }
    }
}
