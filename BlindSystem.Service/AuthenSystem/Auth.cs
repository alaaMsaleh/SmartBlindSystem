using BlindSystem.Identities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlindSystem.Service.AuthenSystem
{
    public class Auth : IAuth
    {
        private readonly IConfiguration _configuration;

        public Auth(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> CreateToken(ApplicationUser user, UserManager<ApplicationUser> userManager)
        {



            //create Claims 
            var AuthClaim = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.DisplayName),
                new Claim(ClaimTypes.Email,user.EmailAddress)
            };

            var userRole = await userManager.GetRolesAsync(user);
            foreach (var claim in userRole)
            {
                AuthClaim.Add(new Claim(ClaimTypes.Role, claim));
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issure"],
                audience: _configuration["JWT:ValiedAudience"],
                claims: AuthClaim,
                expires: DateTime.Now.AddMinutes(double.Parse(_configuration["JWT:DurationInMintes"])),
                signingCredentials: signingCredentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = tokenHandler.WriteToken(token);

            return await Task.FromResult(tokenString);



        }
    }
}