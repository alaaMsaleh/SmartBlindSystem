using BlindSystem.Domain.Entities;
using BlindSystem.Domain.Service_Contract;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace BlindSystem.Service.Services
{
    public class UserService : IUserService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<UserService> _logger;

        public UserService(
            UserManager<ApplicationUser> userManager,
            ILogger<UserService> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }


        public async Task<(bool Succeeded, string? ErrorMessage)> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
            {
                _logger.LogWarning("ChangePassword: user {UserId} not found.", userId);
                return (false, "User not found.");
            }

            // Delegate password verification + hashing entirely to ASP.NET Identity
            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                _logger.LogWarning("ChangePassword failed for user {UserId}: {Errors}", userId, errors);
                return (false, errors);
            }

            _logger.LogInformation("ChangePassword succeeded for user {UserId}.", userId);
            return (true, null);
        }


        public async Task<ApplicationUser?> UpdateProfileAsync(Guid userId, ApplicationUser updatedData)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
            {
                _logger.LogWarning("UpdateProfile: user {UserId} not found.", userId);
                return null;
            }


            if (!string.IsNullOrWhiteSpace(updatedData.FullName))
                user.FullName = updatedData.FullName;

            if (!string.IsNullOrWhiteSpace(updatedData.DisplayName))
                user.DisplayName = updatedData.DisplayName;

            if (!string.IsNullOrWhiteSpace(updatedData.PhoneNumber))
                user.PhoneNumber = updatedData.PhoneNumber;

            if (!string.IsNullOrWhiteSpace(updatedData.UserImage))
                user.UserImage = updatedData.UserImage;

            if (!string.IsNullOrWhiteSpace(updatedData.Gender))
                user.Gender = updatedData.Gender;

            if (updatedData.BirthDate != default)
                user.BirthDate = updatedData.BirthDate;

            if (!string.IsNullOrWhiteSpace(updatedData.Email)
                && !string.Equals(user.Email, updatedData.Email, StringComparison.OrdinalIgnoreCase))
            {
                var token = await _userManager.GenerateChangeEmailTokenAsync(user, updatedData.Email);
                var emailResult = await _userManager.ChangeEmailAsync(user, updatedData.Email, token);
                if (!emailResult.Succeeded)
                {
                    var errors = string.Join("; ", emailResult.Errors.Select(e => e.Description));
                    _logger.LogWarning("UpdateProfile email change failed for user {UserId}: {Errors}", userId, errors);
                    // Return null so the controller can surface the failure
                    return null;
                }
                // Keep UserName in sync with email (existing project convention)
                user.UserName = updatedData.Email.Split('@')[0];
                await _userManager.UpdateNormalizedUserNameAsync(user);
            }

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                _logger.LogWarning("UpdateProfile failed for user {UserId}: {Errors}", userId, errors);
                return null;
            }

            _logger.LogInformation("UpdateProfile succeeded for user {UserId}.", userId);
            return user;
        }


        public async Task<ApplicationUser?> GetProfileAsync(Guid userId)
        {
            return await _userManager.FindByIdAsync(userId.ToString());
        }
    }
}

