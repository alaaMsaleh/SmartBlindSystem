using BlindSystem.Domain.Entities;

namespace BlindSystem.Domain.Service_Contract
{
    public interface IUserService
    {
        Task<(bool Succeeded, string? ErrorMessage)> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);
        Task<ApplicationUser?> UpdateProfileAsync(Guid userId, ApplicationUser updatedData);
        Task<ApplicationUser?> GetProfileAsync(Guid userId);
    }
}
