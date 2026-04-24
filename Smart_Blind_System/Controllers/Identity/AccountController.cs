using BlindSystem.Domain.Entities;
using BlindSystem.Domain.Service_Contract;
using BlindSystem.Service.AuthenSystem;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Smart_Blind_System.API.DTOs.IdentityUser;
using Smart_Blind_System.API.Error;
using Smart_Blind_System.API.MailService;
using Smart_Blind_System.API.Mapping;
using System.Security.Claims;

namespace Smart_Blind_System.API.Controllers.Identity
{
    public class AccountController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAuth _auth;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IMailService _mailService;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IAuth aut, IConfiguration configuration
            , IUserService userService, IMailService mailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _auth = aut;
            _configuration = configuration;
            _userService = userService;
            _mailService = mailService;
        }



        //Create Registeration
        [HttpPost("CreateNewAccount")]

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

        [HttpPost("google-login")]
        public async Task<ActionResult> GoogleLogin([FromBody] string idToken)
        {
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new List<string>() { _configuration["Google:ClientId"] }
                };

                var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);

                var user = await _userManager.FindByEmailAsync(payload.Email);

                if (user == null)
                {
                    user = new ApplicationUser
                    {
                        UserName = payload.Email,
                        Email = payload.Email,
                        DisplayName = payload.Name
                    };

                    var result = await _userManager.CreateAsync(user);
                    if (!result.Succeeded) return BadRequest("Failed to create user.");

                    await _userManager.AddLoginAsync(user, new UserLoginInfo("Google", payload.Subject, "Google"));
                }

                // Use the existing _auth.CreateToken method to generate the JWT token
                var myJwtToken = await _auth.CreateToken(user, _userManager);

                return Ok(new { token = myJwtToken });
            }
            catch (InvalidJwtException)
            {
                return BadRequest("Invalid Google Token.");
            }
        }

        [Authorize]
        [HttpGet("profile")]
        [ProducesResponseType(typeof(UserResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserResponseDto>> GetProfile()
        {
            var userId = GetAuthenticatedUserId();
            if (userId == Guid.Empty)
                return Unauthorized(new ApiResponse(401));

            var user = await _userService.GetProfileAsync(userId);
            if (user is null)
                return NotFound(new ApiResponse(404, "User profile not found."));

            return Ok(user.ToResponseDto());
        }

        [Authorize]
        [HttpPut("UpdateProfile")]

        public async Task<ActionResult<UserResponseDto>> UpdateProfile([FromForm] UpdateUserProfileDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = GetAuthenticatedUserId();
            if (userId == Guid.Empty)
                return Unauthorized(new ApiResponse(401));

            // Map DTO → lightweight entity; service applies the changes selectively
            var updatedData = dto.ToEntity();
            var updatedUser = await _userService.UpdateProfileAsync(userId, updatedData);

            if (updatedUser is null)
                return BadRequest(new ApiResponse(400, "Profile update failed. Please verify your input."));

            return Ok(updatedUser.ToResponseDto());
        }


        // Change Password

        [Authorize]
        [HttpPost("change-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = GetAuthenticatedUserId();
            if (userId == Guid.Empty)
                return Unauthorized(new ApiResponse(401));

            var (succeeded, errorMessage) = await _userService.ChangePasswordAsync(
                userId,
                dto.CurrentPassword,
                dto.NewPassword);

            if (!succeeded)
                return BadRequest(new ApiResponse(400, errorMessage));

            return Ok(new { Message = "Password changed successfully." });
        }

        [HttpPost("forgot-password")]
        [AllowAnonymous]
        public async Task<ActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            //Find user at Db


            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null) return Ok(new ApiResponse(200, "If your email is in our system, you will receive a reset link."));

            //Generte token 
            var token = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");
            var body = $@"
    <div style='font-family: Arial;'>
        <h2>Smart Blind System</h2>
        <p>You requested to reset your password. Use the code below:</p>
        <h3 style='color: blue;'>{token}</h3>
        <p>If you didn't request this, please ignore this email.</p>
    </div>";

            await _mailService.SendEmailAsync(user.Email, "Reset Your Password", body);



            return Ok(new ApiResponse(200, "Reset link sent successfully."));
        }



        [HttpPost("reset-password")]
        public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);


            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null) return BadRequest("Invalid Request");

            // 2. (PIN Code)
            var isValid = await _userManager.VerifyTwoFactorTokenAsync(user, "Email", dto.Token);

            if (!isValid)
            {
                return BadRequest("The code is incorrect or has expired.");
            }


            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, resetToken, dto.NewPassword);

            if (result.Succeeded)
            {
                return Ok(new ApiResponse(200, "Password has been reset successfully."));
            }

            return BadRequest(result.Errors);
        }















        // Private Helpers


        private Guid GetAuthenticatedUserId()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.TryParse(claim, out var id) ? id : Guid.Empty;
        }

    }
}

