using System.Collections.Generic;
using System.Threading.Tasks;
using VirtualCatalogAPI.Models.Users;

namespace VirtualCatalogAPI.Data.Repository.Users
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User> GetByIdAsync(long id);
        Task<User> GetByIdentificationAsync(string identification);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(long id);
    }
}
