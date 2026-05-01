using BlindSystem.Domain.Entities.MedicalEntity;

namespace BlindSystem.Infrastructure.Specification
{
    public class MedicationByIdSpec : BaseSpecification<Medication>
    {
        public MedicationByIdSpec(Guid medicationId, Guid medicalProfileId)
            : base(m => m.Id == medicationId
                     && m.MedicalProfileId == medicalProfileId)
        { }
    }
}
