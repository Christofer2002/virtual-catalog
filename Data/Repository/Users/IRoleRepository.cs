using VirtualCatalogAPI.Models.Users;

namespace VirtualCatalogAPI.Data.Repository.Users
{
    public interface IRoleRepository
    {
        Task<List<Role>> GetAllRolesAsync();
        Task<Role> GetRoleByIdAsync(long id);
        Task<long> CreateRoleAsync(Role role);
        Task UpdateRoleAsync(Role role);
        Task DeleteRoleAsync(long id);
    }
}
