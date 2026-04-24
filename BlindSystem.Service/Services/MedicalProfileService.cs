using BlindSystem.Domain.Entities;
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


        public async Task<MedicalProfile> GetFullProfileAsync(Guid userId)
        {

            var spec = new MedicalProfileWithDetailsSpec(userId);


            return await _UnitOfWork.Repository<MedicalProfile>().GetEntityWithSpecAsync(spec);
        }
        public Task<MedicalProfile?> UpdateMedicalProfile(MedicalProfile? MedicalProfile)
        {
            throw new NotImplementedException();
        }
    }
}
