using BlindSystem.Domain.Entities.MedicalEntities;
using BlindSystem.Domain.Entities.MedicalEntity;

namespace BlindSystem.Domain.Service_Contract.MedicalProfileInterface
{
    public interface IMedicalProfileService
    {
        Task<MedicalProfile?> CreateMedicalProfile(Guid UserId, MedicalProfile MedicalProfile);

        Task<MedicalProfile?> UpdateMedicalProfile(Guid userId, MedicalProfile? MedicalProfile);


        Task<MedicalProfile?> GetFullProfileAsync(Guid userId);


        //Allergy

        Task<Allergy?> CreateNewAllergy(Guid userId, Allergy? Allergy);
        Task<IEnumerable<Allergy>> GetAllergies(Guid userId, string? severity = null);

        Task<Allergy?> UpdateAllergy(Guid userId, Guid allergyId, Allergy updated);
        Task<bool> DeleteAllergy(Guid userId, Guid allergyId);

    }
}
