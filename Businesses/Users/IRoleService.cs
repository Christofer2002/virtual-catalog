using VirtualCatalogAPI.Models.Users;

namespace VirtualCatalogAPI.Businesses.Users
{
    public interface IRoleService
    {
        Task<List<Role>> GetAllRolesAsync();
        Task<Role> GetRoleByIdAsync(long id);
        Task<Role> CreateRoleAsync(Role role);
        Task UpdateRoleAsync(Role role);
        Task DeleteRoleAsync(long id);
    }

}
