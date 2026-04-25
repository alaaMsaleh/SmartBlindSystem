using BlindSystem.Domain.Entities.MedicalEntities;

namespace BlindSystem.Infrastructure.Specification
{
    public class AllergyByIdSpec : BaseSpecification<Allergy>
    {
        public AllergyByIdSpec(Guid allergyId, Guid medicalProfileId)
            : base(a => a.Id == allergyId
                     && a.MedicalProfileId == medicalProfileId)
        { }
    }
}
