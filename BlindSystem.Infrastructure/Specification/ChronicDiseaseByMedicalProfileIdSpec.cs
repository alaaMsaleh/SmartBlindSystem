using BlindSystem.Domain.Entities.MedicalEntities;

namespace BlindSystem.Infrastructure.Specification
{
    public class ChronicDiseaseByMedicalProfileIdSpec : BaseSpecification<ChronicDisease>
    {
        public ChronicDiseaseByMedicalProfileIdSpec() { }

        // All Chronic Diseases
        public ChronicDiseaseByMedicalProfileIdSpec(Guid medicalProfileId)
            : base(d => d.MedicalProfileId == medicalProfileId)
        {
        }
    }
}


