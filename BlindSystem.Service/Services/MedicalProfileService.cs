using BlindSystem.Domain.Entities;
using BlindSystem.Domain.Entities.MedicalEntities;
using BlindSystem.Domain.Entities.MedicalEntity;
using BlindSystem.Domain.Interfaces;
using BlindSystem.Domain.Service_Contract.MedicalProfileInterface;
using BlindSystem.Infrastructure.Specification;
using Microsoft.AspNetCore.Identity;

namespace BlindSystem.Service.Services
{
    public class MedicalProfileService : IMedicalProfileService
    {
        //at service i not work with repository direct i inject IUnitofwork

        private readonly IUnitOfWork _UnitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public MedicalProfileService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _UnitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<MedicalProfile?> CreateMedicalProfile(Guid userId, MedicalProfile medicalProfile)
        {

            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null) return null;

            var spec = new MedicalProfileWithDetailsSpec(userId);
            var existingProfile = await _UnitOfWork.Repository<MedicalProfile>().GetEntityWithSpecAsync(spec);

            if (existingProfile is not null) return existingProfile;


            medicalProfile.UserId = userId;


            _UnitOfWork.Repository<MedicalProfile>().Add(medicalProfile);

            var result = await _UnitOfWork.CompleteAsync();

            return result > 0 ? medicalProfile : null;
        }


        public async Task<MedicalProfile?> GetFullProfileAsync(Guid userId)
        {

            var spec = new MedicalProfileWithDetailsSpec(userId);


            return await _UnitOfWork.Repository<MedicalProfile>().GetEntityWithSpecAsync(spec);
        }




        public async Task<MedicalProfile?> UpdateMedicalProfile(Guid userId, MedicalProfile? MedicalProfile)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null) return null;

            var existing = await _UnitOfWork.Repository<MedicalProfile>().GetEntityWithSpecAsync(new MedicalProfileWithDetailsSpec(userId));
            if (existing is null) return null;
            existing.BloodType = MedicalProfile.BloodType;
            existing.Height = MedicalProfile.Height;
            existing.Weight = MedicalProfile.Weight;
            existing.Notes = MedicalProfile.Notes;
            existing.DrPhone = MedicalProfile.DrPhone;
            existing.Gender = MedicalProfile.Gender;
            _UnitOfWork.Repository<MedicalProfile>().Update(existing);
            var result = await _UnitOfWork.CompleteAsync(); return result > 0 ? existing : null;


        }



        public async Task<Allergy?> CreateNewAllergy(Guid userId, Allergy? Allergy)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null) return null;

            var profile = await _UnitOfWork.Repository<MedicalProfile>().GetEntityWithSpecAsync(new MedicalProfileWithDetailsSpec(userId));
            if (profile is null) return null;
            Allergy.MedicalProfileId = profile.Id;
            if (profile is null) return null;
            var newallergy = _UnitOfWork.Repository<Allergy>().Add(Allergy);
            var result = await _UnitOfWork.CompleteAsync(); return result > 0 ? newallergy : null;

        }

        //// Get All Allergies
        public async Task<IEnumerable<Allergy>> GetAllergies(Guid userId, string? severity = null)
        {
            {
                var profile = await _UnitOfWork.Repository<MedicalProfile>()
                    .GetEntityWithSpecAsync(new MedicalProfileWithDetailsSpec(userId));

                if (profile is null) return Enumerable.Empty<Allergy>();
                var spec = severity is null
                        ? new AllergyByMedicalProfileIdSpec(profile.Id)
                        : new AllergyByMedicalProfileIdSpec(profile.Id, severity);

                return await _UnitOfWork.Repository<Allergy>()
                    .GetAllWithSpecAsync(spec);



            }

        }

        public async Task<Allergy?> UpdateAllergy(Guid userId, Guid allergyId, Allergy updated)
        {
            var profile = await _UnitOfWork.Repository<MedicalProfile>()
                .GetEntityWithSpecAsync(new MedicalProfileWithDetailsSpec(userId));

            if (profile is null) return null;

            var spec = new AllergyByIdSpec(allergyId, profile.Id);
            var existing = await _UnitOfWork.Repository<Allergy>()
                .GetEntityWithSpecAsync(spec);

            if (existing is null) return null;

            existing.Name = updated.Name;
            existing.Severity = updated.Severity;
            existing.Reaction = updated.Reaction;

            _UnitOfWork.Repository<Allergy>().Update(existing);
            var result = await _UnitOfWork.CompleteAsync();

            return result > 0 ? existing : null;
        }

        public async Task<bool> DeleteAllergy(Guid userId, Guid allergyId)
        {
            var profile = await _UnitOfWork.Repository<MedicalProfile>()
                .GetEntityWithSpecAsync(new MedicalProfileWithDetailsSpec(userId));

            if (profile is null) return false;

            var spec = new AllergyByIdSpec(allergyId, profile.Id);
            var allergy = await _UnitOfWork.Repository<Allergy>()
                .GetEntityWithSpecAsync(spec);

            if (allergy is null) return false;

            await _UnitOfWork.Repository<Allergy>().DeleteAsync(allergy.Id);
            var result = await _UnitOfWork.CompleteAsync();

            return result > 0;
        }


        //// ChronicDisease

        public async Task<ChronicDisease?> CreateChronicDisease(Guid userId, ChronicDisease? chronicDisease)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null) return null;

            var profile = await _UnitOfWork.Repository<MedicalProfile>().GetEntityWithSpecAsync(new MedicalProfileWithDetailsSpec(userId));
            if (profile is null) return null;

            chronicDisease.MedicalProfileId = profile.Id;

            var newDisease = _UnitOfWork.Repository<ChronicDisease>().Add(chronicDisease);
            var result = await _UnitOfWork.CompleteAsync();
            return result > 0 ? newDisease : null;
        }

        public async Task<IEnumerable<ChronicDisease>> GetChronicDiseases(Guid userId)
        {
            var profile = await _UnitOfWork.Repository<MedicalProfile>()
                .GetEntityWithSpecAsync(new MedicalProfileWithDetailsSpec(userId));

            if (profile is null) return Enumerable.Empty<ChronicDisease>();

            var spec = new ChronicDiseaseByMedicalProfileIdSpec(profile.Id);

            return await _UnitOfWork.Repository<ChronicDisease>()
                .GetAllWithSpecAsync(spec);
        }

        public async Task<ChronicDisease?> UpdateChronicDisease(Guid userId, Guid diseaseId, ChronicDisease updated)
        {
            var profile = await _UnitOfWork.Repository<MedicalProfile>()
                .GetEntityWithSpecAsync(new MedicalProfileWithDetailsSpec(userId));

            if (profile is null) return null;

            var spec = new ChronicDiseaseByIdSpec(diseaseId, profile.Id);
            var existing = await _UnitOfWork.Repository<ChronicDisease>()
                .GetEntityWithSpecAsync(spec);

            if (existing is null) return null;

            existing.Name = updated.Name;
            existing.Description = updated.Description;
            existing.DiagnosedDate = updated.DiagnosedDate;

            _UnitOfWork.Repository<ChronicDisease>().Update(existing);
            var result = await _UnitOfWork.CompleteAsync();

            return result > 0 ? existing : null;
        }

        public async Task<bool> DeleteChronicDisease(Guid userId, Guid diseaseId)
        {
            var profile = await _UnitOfWork.Repository<MedicalProfile>()
                .GetEntityWithSpecAsync(new MedicalProfileWithDetailsSpec(userId));

            if (profile is null) return false;

            var spec = new ChronicDiseaseByIdSpec(diseaseId, profile.Id);
            var disease = await _UnitOfWork.Repository<ChronicDisease>()
                .GetEntityWithSpecAsync(spec);

            if (disease is null) return false;

            await _UnitOfWork.Repository<ChronicDisease>().DeleteAsync(disease.Id);
            var result = await _UnitOfWork.CompleteAsync();

            return result > 0;
        }


        //// MedicalHistory

        public async Task<MedicalHistoryEntry?> CreateMedicalHistoryEntry(Guid userId, MedicalHistoryEntry? entry)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null) return null;

            var profile = await _UnitOfWork.Repository<MedicalProfile>().GetEntityWithSpecAsync(new MedicalProfileWithDetailsSpec(userId));
            if (profile is null) return null;

            entry.MedicalProfileId = profile.Id;

            var newEntry = _UnitOfWork.Repository<MedicalHistoryEntry>().Add(entry);
            var result = await _UnitOfWork.CompleteAsync();
            return result > 0 ? newEntry : null;
        }

        public async Task<IEnumerable<MedicalHistoryEntry>> GetMedicalHistoryEntries(Guid userId)
        {
            var profile = await _UnitOfWork.Repository<MedicalProfile>()
                .GetEntityWithSpecAsync(new MedicalProfileWithDetailsSpec(userId));

            if (profile is null) return Enumerable.Empty<MedicalHistoryEntry>();

            var spec = new MedicalHistoryByMedicalProfileIdSpec(profile.Id);

            return await _UnitOfWork.Repository<MedicalHistoryEntry>()
                .GetAllWithSpecAsync(spec);
        }

        public async Task<MedicalHistoryEntry?> UpdateMedicalHistoryEntry(Guid userId, Guid entryId, MedicalHistoryEntry updated)
        {
            var profile = await _UnitOfWork.Repository<MedicalProfile>()
                .GetEntityWithSpecAsync(new MedicalProfileWithDetailsSpec(userId));

            if (profile is null) return null;

            var spec = new MedicalHistoryByIdSpec(entryId, profile.Id);
            var existing = await _UnitOfWork.Repository<MedicalHistoryEntry>()
                .GetEntityWithSpecAsync(spec);

            if (existing is null) return null;

            existing.Title = updated.Title;
            existing.Description = updated.Description;
            existing.EventDate = updated.EventDate;
            existing.DoctorName = updated.DoctorName;

            _UnitOfWork.Repository<MedicalHistoryEntry>().Update(existing);
            var result = await _UnitOfWork.CompleteAsync();

            return result > 0 ? existing : null;
        }

        public async Task<bool> DeleteMedicalHistoryEntry(Guid userId, Guid entryId)
        {
            var profile = await _UnitOfWork.Repository<MedicalProfile>()
                .GetEntityWithSpecAsync(new MedicalProfileWithDetailsSpec(userId));

            if (profile is null) return false;

            var spec = new MedicalHistoryByIdSpec(entryId, profile.Id);
            var entry = await _UnitOfWork.Repository<MedicalHistoryEntry>()
                .GetEntityWithSpecAsync(spec);

            if (entry is null) return false;

            await _UnitOfWork.Repository<MedicalHistoryEntry>().DeleteAsync(entry.Id);
            var result = await _UnitOfWork.CompleteAsync();

            return result > 0;
        }


        //// Medication

        public async Task<Medication?> CreateMedication(Guid userId, Medication? medication)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null) return null;

            var profile = await _UnitOfWork.Repository<MedicalProfile>().GetEntityWithSpecAsync(new MedicalProfileWithDetailsSpec(userId));
            if (profile is null) return null;

            medication.MedicalProfileId = profile.Id;

            var newMedication = _UnitOfWork.Repository<Medication>().Add(medication);
            var result = await _UnitOfWork.CompleteAsync();
            return result > 0 ? newMedication : null;
        }

        public async Task<IEnumerable<Medication>> GetMedications(Guid userId)
        {
            var profile = await _UnitOfWork.Repository<MedicalProfile>()
                .GetEntityWithSpecAsync(new MedicalProfileWithDetailsSpec(userId));

            if (profile is null) return Enumerable.Empty<Medication>();

            var spec = new MedicationByMedicalProfileIdSpec(profile.Id);

            return await _UnitOfWork.Repository<Medication>()
                .GetAllWithSpecAsync(spec);
        }

        public async Task<Medication?> UpdateMedication(Guid userId, Guid medicationId, Medication updated)
        {
            var profile = await _UnitOfWork.Repository<MedicalProfile>()
                .GetEntityWithSpecAsync(new MedicalProfileWithDetailsSpec(userId));

            if (profile is null) return null;

            var spec = new MedicationByIdSpec(medicationId, profile.Id);
            var existing = await _UnitOfWork.Repository<Medication>()
                .GetEntityWithSpecAsync(spec);

            if (existing is null) return null;

            existing.Name = updated.Name;
            existing.Dosage = updated.Dosage;
            existing.Schedule = updated.Schedule;

            _UnitOfWork.Repository<Medication>().Update(existing);
            var result = await _UnitOfWork.CompleteAsync();

            return result > 0 ? existing : null;
        }

        public async Task<bool> DeleteMedication(Guid userId, Guid medicationId)
        {
            var profile = await _UnitOfWork.Repository<MedicalProfile>()
                .GetEntityWithSpecAsync(new MedicalProfileWithDetailsSpec(userId));

            if (profile is null) return false;

            var spec = new MedicationByIdSpec(medicationId, profile.Id);
            var medication = await _UnitOfWork.Repository<Medication>()
                .GetEntityWithSpecAsync(spec);

            if (medication is null) return false;

            await _UnitOfWork.Repository<Medication>().DeleteAsync(medication.Id);
            var result = await _UnitOfWork.CompleteAsync();

            return result > 0;
        }



    }
}


