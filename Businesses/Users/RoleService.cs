using VirtualCatalogAPI.Models.Users;
using VirtualCatalogAPI.Data.Repository.Users;

namespace VirtualCatalogAPI.Businesses.Users
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
        }

        public async Task<List<Role>> GetAllRolesAsync()
        {
            try
            {
                var roles = await _roleRepository.GetAllRolesAsync();
                if (roles == null)
                    throw new Exception("No roles found.");
                return roles;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in RoleService.GetAllRolesAsync: {ex.Message}");
                throw new Exception("An error occurred while retrieving roles.", ex);
            }
        }

        public async Task<Role> GetRoleByIdAsync(long id)
        {
            if (id <= 0)
                throw new ArgumentException("Id must be greater than zero.", nameof(id));

            try
            {
                var role = await _roleRepository.GetRoleByIdAsync(id);
                if (role == null)
                    throw new Exception($"Role with ID {id} not found.");
                return role;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in RoleService.GetRoleByIdAsync: {ex.Message}");
                throw new Exception("An error occurred while retrieving the role by ID.", ex);
            }
        }

        public async Task<long> CreateRoleAsync(Role role)
        {
            if (role == null || string.IsNullOrWhiteSpace(role.Name))
                throw new ArgumentException("Role name cannot be null or empty.", nameof(role));

            try
            {
                return await _roleRepository.CreateRoleAsync(role);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in RoleService.CreateRoleAsync: {ex.Message}");
                throw new Exception("An error occurred while creating the role.", ex);
            }
        }

        public async Task UpdateRoleAsync(Role role)
        {
            if (role == null || string.IsNullOrWhiteSpace(role.Name) || role.Id <= 0)
                throw new ArgumentException("Invalid role data.", nameof(role));

            try
            {
                await _roleRepository.UpdateRoleAsync(role);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in RoleService.UpdateRoleAsync: {ex.Message}");
                throw new Exception("An error occurred while updating the role.", ex);
            }
        }

        public async Task DeleteRoleAsync(long id)
        {
            if (id <= 0)
                throw new ArgumentException("Id must be greater than zero.", nameof(id));

            try
            {
                await _roleRepository.DeleteRoleAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in RoleService.DeleteRoleAsync: {ex.Message}");
                throw new Exception("An error occurred while deleting the role.", ex);
            }
        }

        Task<Role> IRoleService.CreateRoleAsync(Role role)
        {
            throw new NotImplementedException();
        }
    }
}
