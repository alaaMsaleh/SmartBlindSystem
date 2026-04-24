using BlindSystem.Domain.Entities.MedicalEntity;

namespace BlindSystem.Infrastructure.Specification
{
    public class MedicalProfileWithDetailsSpec : BaseSpecification<MedicalProfile>
    {
        public MedicalProfileWithDetailsSpec(Guid userId) : base(x => x.UserId == userId)
        {
            AddInclude(x => x.Allergies);
            AddInclude(x => x.ChronicDiseases);
            AddInclude(x => x.HistoryEntries);

        }
    }
}
