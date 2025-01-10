using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using VirtualCatalogAPI.Businesses.Users;
using VirtualCatalogAPI.Models.Users;

namespace VirtualCatalogAPI.Controllers.Users
{
    [ApiController]
    [Route("user")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        // Get all users
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                return Ok(users); // HTTP 200
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "An error occurred while retrieving users.");
            }
        }

        // Get user by Id
        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                if (id <= 0) return BadRequest("Invalid User ID.");

                var user = await _userService.GetUserByIdAsync(id);
                if (user == null) return NotFound("User not found.");

                return Ok(user); // HTTP 200
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "An error occurred while retrieving the user.");
            }
        }

        // Add a new user
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] User user)
        {
            try
            {
                if (user == null || string.IsNullOrWhiteSpace(user.Name) || string.IsNullOrWhiteSpace(user.Email))
                    return BadRequest("User name and email are required.");

                await _userService.AddUserAsync(user);
                return CreatedAtAction(nameof(GetById), new { id = user.Id }, user); // HTTP 201
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Validation Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "An error occurred while adding the user.");
            }
        }

        // Update an existing user
        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, [FromBody] User user)
        {
            try
            {
                if (id <= 0 || user == null || id != user.Id)
                    return BadRequest("Invalid user data.");

                await _userService.UpdateUserAsync(user);
                return NoContent(); // HTTP 204
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Validation Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "An error occurred while updating the user.");
            }
        }

        // Delete a user
        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                if (id <= 0) return BadRequest("Invalid User ID.");

                await _userService.DeleteUserAsync(id);
                return NoContent(); // HTTP 204
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Validation Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "An error occurred while deleting the user.");
            }
        }

        // Get user by Identification
        [HttpGet("identification/{identification}")]
        public async Task<IActionResult> GetByIdentification(string identification)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(identification))
                    return BadRequest("Identification is required.");

                var user = await _userService.GetUserByIdentificationAsync(identification);
                if (user == null) return NotFound("User not found.");

                return Ok(user); // HTTP 200
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "An error occurred while retrieving the user by identification.");
            }
        }
    }
}
