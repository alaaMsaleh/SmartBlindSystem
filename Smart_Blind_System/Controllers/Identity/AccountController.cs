using BlindSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Smart_Blind_System.API.DTOs.IdentityUser;

namespace Smart_Blind_System.API.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }

        //Create Registeration
        [HttpPost("register")]

        public async Task<ActionResult<UserDto>> Registration(RegistrationDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);


            }

            //Check iif email already exists
            var exisingUser = await _userManager.FindByEmailAsync(userDto.Email);
            if (exisingUser != null) return BadRequest(new { Message = "This Email is already registered" });


            //Check  userName

            var userName = userDto.Email.Split('@')[0];
            var existingUserName = await _userManager.FindByNameAsync(userName);
            if (existingUserName != null)
                return BadRequest(new { message = "This username is already taken." });


            var User = new ApplicationUser
            {
                DisplayName = userDto.DisplayName,
                Email = userDto.Email,
                UserName = userDto.Email.Split("@")[0],

            };

            var result = await _userManager.CreateAsync(User, userDto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new UserDto()
            {
                DispalyName = User.DisplayName,
                Email = userDto.Email,
                Token = "Jwt-Token "

            });
        }


        [HttpPost("sigin")]
        public async Task<ActionResult<UserDto>> Sigin(SiginDto sigin)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);


            var user = await _userManager.FindByEmailAsync(sigin.Email);
            if (user == null) return BadRequest(new { message = "This userEmail notFound" });

            var userPassword = await _signInManager.CheckPasswordSignInAsync(user, sigin.Password, false);

            if (userPassword == null) return BadRequest(new { message = " unCorrect Password" });

            var UserSigin = new UserDto()
            {
                DispalyName = user.DisplayName,
                Email = user.Email,
                Token = ""
            };

            return Ok(UserSigin);

        }

    }
}
