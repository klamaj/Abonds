using API.Services.Interfaces;
using Core.DTOs;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers.Auth
{
    public class AuthController : BaseApiController
    {
        private readonly ILogger<AuthController> _logger;
        private readonly UserManager<UserModel> _userManager;
        private readonly SignInManager<UserModel> _signInManager;
        private readonly ITokenService _tokenService;
        public AuthController(ILogger<AuthController> logger, UserManager<UserModel> userManager, SignInManager<UserModel> signInManager, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        /// <summary>
        /// User login
        /// </summary>
        /// <param name="val" example="Username and password">UserLoginDto</param>
        /// <returns>UserDto</returns>
        /// <response code="200">User successfully loggedin</response>
        /// POST: /Auth/login
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto val)
        {

            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.UserName == val.Email!.ToLower());

            if (user is null) return Unauthorized("Invalid User");

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
    }
}