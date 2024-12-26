using VirtualCatalogAPI.Models.Users;

namespace VirtualCatalogAPI.Data.Repository.Auth
{
    public interface IAuthRepository
    {
        Task<User> GetUserByEmailAsync(string email);
        Task CreateUserAsync(User user, int roleId);
    }

}
