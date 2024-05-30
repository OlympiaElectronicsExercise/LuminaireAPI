using API.DTOs;
using API.Interfaces;
using API.Models;
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
            };
        }

        /// <summary>
        /// User Registration
        /// </summary>
        /// <param name="val" example="Username and password">LoginDto</param>
        /// <returns>UserDto</returns>
        /// <response code="200">User successfully registered</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Auth/register
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
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDto>> Register(LoginDto val)
        {
            if (await UserExistsAsync(val.Email!.ToLower())) return BadRequest("User already exists");

            var user = new UserModel
            {
                Email = val.Email,
                UserName = val.Email
            };

            await _userManager.CreateAsync(user, val.Password!);

            var result = await _signInManager.CheckPasswordSignInAsync(user, val.Password!, false);

            if (result.Succeeded) _logger.LogInformation($"User: {user.Email} logged in successfully");

            return new UserDto
            {
                Email = user.Email,
                Token = await _tokenService.CreateToken(user),
            };
        }

        // Check of user exists
        private async Task<bool> UserExistsAsync(string email)
        {
            return await _userManager.Users.AnyAsync(u => u.Email == email.ToLower());
        }
    }
}
