using System.Collections.Generic;
using System.Threading.Tasks;
using VirtualCatalogAPI.Models.Products;

namespace VirtualCatalogAPI.Data.Repository.Products
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<Product> GetProductByIdAsync(long id);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(long id);
        Task<List<Product>> GetByCategoryAsync(long categoryId);
    }
}
