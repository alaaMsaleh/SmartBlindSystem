using BlindSystem.Domain.Entities;
using BlindSystem.Service.AuthenSystem;
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
        private readonly IAuth _auth;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IAuth auth)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _auth = auth;
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
                FullName = userDto.FullName,
                DisplayName = userDto.FullName,
                Email = userDto.Email,
                UserName = userDto.Email.Split("@")[0],
                PhoneNumber = userDto.PhoneNumber,
                Gender = userDto.Gender


            };

            var result = await _userManager.CreateAsync(User, userDto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new UserDto()
            {
                FullName = User.DisplayName,
                Email = userDto.Email,
                Token = await _auth.CreateToken(User, _userManager)


            });
        }


        [HttpPost("signin")]
        public async Task<ActionResult<UserDto>> SignIn(SignInDto SignIn)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);


            var user = await _userManager.FindByEmailAsync(SignIn.Email);
            if (user == null) return BadRequest(new { message = "This userEmail notFound" });

            var userPassword = await _signInManager.CheckPasswordSignInAsync(user, SignIn.Password, false);

            if (!userPassword.Succeeded) return BadRequest(new { message = " unCorrect Password" });

            var UserSignIn = new UserDto()
            {
                FullName = user.FullName,
                Email = user.Email,
                Token = await _auth.CreateToken(user, _userManager)
            };

            return Ok(UserSignIn);

        }

    }
}
