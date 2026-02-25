using BlindSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace BlindSystem.Service.AuthenSystem
{
    public interface IAuth
    {
        Task<string> CreateToken(ApplicationUser user, UserManager<ApplicationUser> userManager);
    }
}
