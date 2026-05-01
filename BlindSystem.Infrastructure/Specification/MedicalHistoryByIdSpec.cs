using BlindSystem.Domain.Entities.MedicalEntity;

namespace BlindSystem.Infrastructure.Specification
{
    public class MedicalHistoryByIdSpec : BaseSpecification<MedicalHistoryEntry>
    {
        public MedicalHistoryByIdSpec(Guid entryId, Guid medicalProfileId)
            : base(h => h.Id == entryId
                     && h.MedicalProfileId == medicalProfileId)
        { }
    }
}
