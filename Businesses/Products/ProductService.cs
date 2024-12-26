using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VirtualCatalogAPI.Data.Repository.Products;
using VirtualCatalogAPI.Models.Products;

namespace VirtualCatalogAPI.Businesses.Products
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            try
            {
                return await _productRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ProductService.GetAllProductsAsync: {ex.Message}");
                throw new Exception("An error occurred while retrieving products.", ex);
            }
        }

        public async Task<List<Product>> GetProductsByCategoryAsync(long categoryId)
        {
            if (categoryId <= 0)
                throw new ArgumentException("Category ID must be greater than zero.", nameof(categoryId));

            try
            {
                return await _productRepository.GetByCategoryAsync(categoryId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ProductService.GetProductsByCategoryAsync: {ex.Message}");
                throw new Exception("An error occurred while retrieving products by category.", ex);
            }
        }

        public async Task AddProductAsync(Product product)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));

            try
            {
                ValidateProduct(product);
                await _productRepository.AddAsync(product);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Validation Error in ProductService.AddProductAsync: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ProductService.AddProductAsync: {ex.Message}");
                throw new Exception("An error occurred while adding the product.", ex);
            }
        }

        public async Task UpdateProductAsync(Product product)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));

            try
            {
                ValidateProduct(product);
                await _productRepository.UpdateAsync(product);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Validation Error in ProductService.UpdateProductAsync: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ProductService.UpdateProductAsync: {ex.Message}");
                throw new Exception("An error occurred while updating the product.", ex);
            }
        }

        public async Task DeleteProductAsync(long productId)
        {
            if (productId <= 0)
                throw new ArgumentException("Product ID must be greater than zero.", nameof(productId));

            try
            {
                await _productRepository.DeleteAsync(productId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ProductService.DeleteProductAsync: {ex.Message}");
                throw new Exception("An error occurred while deleting the product.", ex);
            }
        }

        public async Task<Product> GetProductByIdAsync(long id)
        {
            if (id <= 0) throw new ArgumentException("Product ID must be greater than zero.", nameof(id));

            try
            {
                var product = await _productRepository.GetProductByIdAsync(id);
                if (product == null)
                    throw new Exception("Product not found.");

                return product;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ProductService.GetProductByIdAsync: {ex.Message}");
                throw new Exception("An error occurred while retrieving the product.", ex);
            }
        }

        private void ValidateProduct(Product product)
        {
            if (string.IsNullOrWhiteSpace(product.Name))
                throw new ArgumentException("Product name is required.", nameof(product.Name));

            if (product.Price <= 0)
                throw new ArgumentException("Product price must be greater than zero.", nameof(product.Price));

            if (product.Stock < 0)
                throw new ArgumentException("Product stock cannot be negative.", nameof(product.Stock));

            if (product.CategoryId <= 0)
                throw new ArgumentException("Product must have a valid category ID.", nameof(product.CategoryId));
        }
    }
}
