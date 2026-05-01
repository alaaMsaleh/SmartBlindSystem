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

        //ChronicDisease

        Task<ChronicDisease?> CreateChronicDisease(Guid userId, ChronicDisease? chronicDisease);
        Task<IEnumerable<ChronicDisease>> GetChronicDiseases(Guid userId);

        Task<ChronicDisease?> UpdateChronicDisease(Guid userId, Guid diseaseId, ChronicDisease updated);
        Task<bool> DeleteChronicDisease(Guid userId, Guid diseaseId);


        //MedicalHistory

        Task<MedicalHistoryEntry?> CreateMedicalHistoryEntry(Guid userId, MedicalHistoryEntry? entry);
        Task<IEnumerable<MedicalHistoryEntry>> GetMedicalHistoryEntries(Guid userId);

        Task<MedicalHistoryEntry?> UpdateMedicalHistoryEntry(Guid userId, Guid entryId, MedicalHistoryEntry updated);
        Task<bool> DeleteMedicalHistoryEntry(Guid userId, Guid entryId);


        //Medication

        Task<Medication?> CreateMedication(Guid userId, Medication? medication);
        Task<IEnumerable<Medication>> GetMedications(Guid userId);

        Task<Medication?> UpdateMedication(Guid userId, Guid medicationId, Medication updated);
        Task<bool> DeleteMedication(Guid userId, Guid medicationId);

    }
}
