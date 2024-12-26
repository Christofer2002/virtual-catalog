using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using VirtualCatalogAPI.Businesses.Auth;
using VirtualCatalogAPI.Models.Auth;

namespace VirtualCatalogAPI.Controllers.Auth
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        /// <summary>
        /// Handles user login.
        /// </summary>
        /// <param name="loginRequest">The login credentials.</param>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] VirtualCatalogAPI.Models.Auth.LoginRequest loginRequest)
        {
            try
            {
                var authResponse = await _authService.AuthenticateAsync(loginRequest.Email, loginRequest.Password);
                return Ok(authResponse);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Handles user registration.
        /// </summary>
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Models.Auth.RegisterRequest registerRequest)
        {
            if (registerRequest == null || string.IsNullOrWhiteSpace(registerRequest.Email) || string.IsNullOrWhiteSpace(registerRequest.Password))
            {
                return BadRequest("All fields are required.");
            }

            try
            {
                var registeredUser = await _authService.RegisterAsync(registerRequest);
                return CreatedAtAction(nameof(Register), new { id = registeredUser.Id }, registeredUser);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Handles user logout.
        /// </summary>
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            try
            {
                _authService.Logout();
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during logout: {ex.Message}");
                return StatusCode(500, "An error occurred during logout.");
            }
        }

        /// <summary>
        /// Checks the current session status.
        /// </summary>
        [HttpGet("session")]
        public IActionResult CheckSession()
        {
            try
            {
                var sessionStatus = _authService.CheckSession();
                return Ok(sessionStatus);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking session: {ex.Message}");
                return StatusCode(500, "An error occurred while checking session.");
            }
        }
    }
}
