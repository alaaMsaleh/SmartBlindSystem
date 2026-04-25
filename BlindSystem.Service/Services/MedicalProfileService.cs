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

            _UnitOfWork.Repository<Allergy>().Delete(allergy.Id);
            var result = await _UnitOfWork.CompleteAsync();

            return result > 0;
        }



    }
}