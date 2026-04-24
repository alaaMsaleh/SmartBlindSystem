using BlindSystem.Domain.Entities.MedicalEntity;

namespace BlindSystem.Domain.Interfaces
{
    public interface IMedicalProfileRepo : IGenericRepository<MedicalProfile>
    {
        Task<MedicalProfile?> GetFullProfileByUserIdAsync(Guid userId);

    }
}
