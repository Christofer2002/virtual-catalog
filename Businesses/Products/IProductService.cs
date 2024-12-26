using System.Collections.Generic;
using System.Threading.Tasks;
using VirtualCatalogAPI.Models.Products;

namespace VirtualCatalogAPI.Businesses.Products
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<List<Product>> GetProductsByCategoryAsync(long categoryId);
        Task<Product> GetProductByIdAsync(long id);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(long productId);
    }
}
