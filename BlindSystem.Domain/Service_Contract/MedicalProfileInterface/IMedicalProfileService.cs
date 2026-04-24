using BlindSystem.Domain.Entities.MedicalEntity;

namespace BlindSystem.Domain.Service_Contract.MedicalProfileInterface
{
    public interface IMedicalProfileService
    {
        Task<MedicalProfile?> CreateMedicalProfile(Guid UserId, MedicalProfile MedicalProfile);

        Task<MedicalProfile?> UpdateMedicalProfile(MedicalProfile? MedicalProfile);

        Task<MedicalProfile> GetFullProfileAsync(Guid userId);

    }
}
