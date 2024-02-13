using System.Net.Mail;
using API.Services.Interfaces;
using Core.DTOs;
using Core.Models;
using Core.Models.EmailModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers.Auth
{
    public class AuthController : BaseApiController
    {
        private readonly ILogger<AuthController> _logger;
        private readonly UserManager<UserModel> _userManager;
        private readonly SignInManager<UserModel> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IEmailSender _emailSender;
        public AuthController(ILogger<AuthController> logger, UserManager<UserModel> userManager, SignInManager<UserModel> signInManager, ITokenService tokenService, IEmailSender emailSender)
        {
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        /// <summary>
        /// User login
        /// </summary>
        /// <param name="val" example="Username and password">LoginDto</param>
        /// <returns>UserDto</returns>
        /// <response code="200">User successfully loggedin</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Auth/login
        ///     {
        ///        "email": "example@example.com",
        ///        "password": "Aa123456!"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns UserDto (successfully logedIn)</response>
        /// <response code="401">Unauthorized user (wrong password)</response>
        /// <response code="404">If the user not found</response>
        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDto>> Login(LoginDto val)
        {

            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.UserName == val.Email!.ToLower());

            if (user is null) return NotFound("Invalid User");

            var result = await _signInManager.CheckPasswordSignInAsync(user, val.Password!, false);

            if (!result.Succeeded) return Unauthorized();

            if (result.Succeeded) _logger.LogInformation($"User: {user.Email} logged in successfully");

            return new UserDto
            {
                Email = user.Email,
                Token = await _tokenService.CreateToken(user),
                Role = "Admin" //TODO: dynamically create role
            };
        }

        /// <summary>
        /// Forgot password request
        /// </summary>
        /// <param name="forgotPassword" example="Email and CLientURI">ForgotPasswordDto</param>
        /// <returns>string</returns>
        /// <response code="200">Forgot password request has been successfully sent</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Auth/forgot-password
        ///     {
        ///        "email": "example@example.com",
        ///        "clientUri": "https://example.com/auth/forgot-password"
        ///     }
        ///
        /// </remarks>
        /// <response code="404">User doesn't exist</response>
        [AllowAnonymous]
        [HttpPost("forgot-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPassword)
        {
            if (!ModelState.IsValid) return BadRequest();

            var user = await _userManager.FindByEmailAsync(forgotPassword.Email!);

            if (user is null) return NotFound("User doesn't exist");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var param = new Dictionary<string, string>
            {
                { "token", token },
                { "email", forgotPassword.Email! }
            };

            var callback = QueryHelpers.AddQueryString(forgotPassword.ClientURI!, param!);
            var message = new MessageModel(new string[] { user.Email! }, "Reset Password token", callback);

            await _emailSender.SendEmailAsync(message);

            return Ok("Forgot password request has been successfully sent.");
        }

        /// <summary>
        /// User Register
        /// </summary>
        /// <param name="val" example="Email and Password">LoginDto</param>
        /// <returns>UserDto</returns>
        /// <response code="200">User has been successfully registered</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Auth/register
        ///     {
        ///        "email": "example@example.com",
        ///        "password": "A123456b!"
        ///     }
        ///
        /// </remarks>
        /// <response code="400">User Already Exists</response>
        /// <response code="400">An error occured while creating the user</response>
        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDto>> Register(LoginDto val)
        {
            if (await UserExistsAsync(val.Email!)) return BadRequest("User already exists");

            var user = new UserModel
            {
                UserName = val.Email!.ToLower(),
                Email = val.Email.ToLower()
            };

            var result = await _userManager.CreateAsync(user, val.Password!);

            if (!result.Succeeded) return BadRequest(result.Errors);

            // var roleResult = await _userManager.AddToRoleAsync(user, "admin");

            // if (!roleResult.Succeeded) return BadRequest(roleResult.Errors);

            return new UserDto
            {
                Email = user.Email,
                Token = await _tokenService.CreateToken(user)
            };
        }

        /// <summary>
        /// Reset Password
        /// </summary>
        /// <param name="resetPassword" example="Email, Password, ConfirmPassword, Token">ResetPasswordDto</param>
        /// <returns>UserDto</returns>
        /// <response code="200">Password reseted successfully</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Auth/reset-password
        ///     {
        ///        "email": "example@example.com",
        ///        "password": "A123456b!",
        ///        "confirmPassword": "A123456b!",
        ///        "token": "some token"
        ///     }
        ///
        /// </remarks>
        /// <response code="404">User not found</response>
        /// <response code="400">An error occured while reseting the password</response>
        [AllowAnonymous]
        [HttpPost("reset-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPassword)
        {
            if (!ModelState.IsValid) return BadRequest();

            var user = await _userManager.FindByEmailAsync(resetPassword.Email!);

            if (user is null) return NotFound("User does not exist");

            var resetPasswordResult = await _userManager.ResetPasswordAsync(user, resetPassword.Token!, resetPassword.Password!);

            if (!resetPasswordResult.Succeeded)
            {
                var errors = resetPasswordResult.Errors.Select(e => e.Description);
                return BadRequest(new { Errors = errors });
            }

            return Ok("Password reseted successfully");
        }

        // Check of user exists
        private async Task<bool> UserExistsAsync(string email)
        {
            return await _userManager.Users.AnyAsync(u => u.Email == email.ToLower());
        }
    }
}