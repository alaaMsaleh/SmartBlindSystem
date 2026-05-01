using BlindSystem.Domain.Entities.MedicalEntities;

namespace BlindSystem.Infrastructure.Specification
{
    public class ChronicDiseaseByIdSpec : BaseSpecification<ChronicDisease>
    {
        public ChronicDiseaseByIdSpec(Guid diseaseId, Guid medicalProfileId)
            : base(d => d.Id == diseaseId
                     && d.MedicalProfileId == medicalProfileId)
        { }
    }
}
