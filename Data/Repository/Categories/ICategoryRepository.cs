using System.Threading.Tasks;
using VirtualCatalogAPI.Models.Categories;

namespace VirtualCatalogAPI.Data.Repository.Categories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(long id);
        Task AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(long id);
    }
}
