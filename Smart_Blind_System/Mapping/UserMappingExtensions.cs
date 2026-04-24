using BlindSystem.Domain.Entities;
using Smart_Blind_System.API.DTOs.IdentityUser;

namespace Smart_Blind_System.API.Mapping
{
    public static class UserMappingExtensions
    {
        public static UserResponseDto ToResponseDto(this ApplicationUser user)
        {
            return new UserResponseDto
            {
                Id = user.Id,
                FullName = user.FullName,
                DisplayName = user.DisplayName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Gender = user.Gender,
                BirthDate = user.BirthDate == default ? null : user.BirthDate,
                UserImage = user.UserImage,
                CreatedAt = user.CreatedAt
            };
        }

        public static ApplicationUser ToEntity(this UpdateUserProfileDto dto)
        {
            return new ApplicationUser
            {
                FullName = dto.FullName ?? string.Empty,
                DisplayName = dto.DisplayName ?? string.Empty,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                UserImage = dto.UserImage,
                Gender = dto.Gender,
                BirthDate = dto.BirthDate ?? default
            };
        }
    }
}
