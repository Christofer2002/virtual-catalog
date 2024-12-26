using System.Collections.Generic;
using System.Threading.Tasks;
using VirtualCatalogAPI.Models.Categories;

namespace VirtualCatalogAPI.Businesses.Categories
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(long id);
        Task AddCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(long id);
    }
}
