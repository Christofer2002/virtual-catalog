using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using VirtualCatalogAPI.Data.Repository.Users;
using VirtualCatalogAPI.Models.Users;
using VirtualCatalogAPI.Businesses.Users;

namespace VirtualCatalogAPI.Controllers.Users
{
    [ApiController]
    [Route("api/role")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService ?? throw new ArgumentNullException(nameof(roleService));
        }

        // GET: api/roles
        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            try
            {
                var roles = await _roleService.GetAllRolesAsync();
                return Ok(roles); // HTTP 200
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "An error occurred while retrieving roles.");
            }
        }

        // GET: api/roles/{id}
        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetRoleById(long id)
        {
            try
            {
                if (id <= 0) return BadRequest("Invalid Role ID.");

                var role = await _roleService.GetRoleByIdAsync(id);
                if (role == null) return NotFound("Role not found.");

                return Ok(role); // HTTP 200
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "An error occurred while retrieving the role.");
            }
        }

        // POST: api/roles
        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] Role role)
        {
            try
            {
                if (role == null || string.IsNullOrWhiteSpace(role.Name))
                    return BadRequest("Role name is required.");

                var createdRole = await _roleService.CreateRoleAsync(role);
                return CreatedAtAction(nameof(GetRoleById), new { id = createdRole.Id }, createdRole); // HTTP 201
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "An error occurred while creating the role.");
            }
        }

        // PUT: api/roles/{id}
        [HttpPut("{id:long}")]
        public async Task<IActionResult> UpdateRole(long id, [FromBody] Role role)
        {
            try
            {
                if (id <= 0 || role == null || id != role.Id)
                    return BadRequest("Invalid role data.");

                await _roleService.UpdateRoleAsync(role);
                return NoContent(); // HTTP 204
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "An error occurred while updating the role.");
            }
        }

        // DELETE: api/roles/{id}
        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteRole(long id)
        {
            try
            {
                if (id <= 0) return BadRequest("Invalid Role ID.");

                await _roleService.DeleteRoleAsync(id);
                return NoContent(); // HTTP 204
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "An error occurred while deleting the role.");
            }
        }
    }
}
