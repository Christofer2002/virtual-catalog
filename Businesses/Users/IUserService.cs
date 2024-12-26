using System.Collections.Generic;
using System.Threading.Tasks;
using VirtualCatalogAPI.Models.Users;

namespace VirtualCatalogAPI.Businesses.Users
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(long id);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(long id);
        Task<User> GetUserByIdentificationAsync(string identification);
    }
}
