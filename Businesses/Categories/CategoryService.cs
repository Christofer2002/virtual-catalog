using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VirtualCatalogAPI.Data.Repository.Categories;
using VirtualCatalogAPI.Models.Categories;

namespace VirtualCatalogAPI.Businesses.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            try
            {
                return await _categoryRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CategoryService.GetAllCategoriesAsync: {ex.Message}");
                throw new Exception("An error occurred while retrieving categories.", ex);
            }
        }

        public async Task<Category> GetCategoryByIdAsync(long id)
        {
            if (id <= 0) throw new ArgumentException("Category ID must be greater than zero.", nameof(id));

            try
            {
                var category = await _categoryRepository.GetByIdAsync(id);
                if (category == null)
                    throw new Exception("Category not found.");

                return category;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CategoryService.GetCategoryByIdAsync: {ex.Message}");
                throw new Exception("An error occurred while retrieving the category.", ex);
            }
        }

        public async Task AddCategoryAsync(Category category)
        {
            if (category == null) throw new ArgumentNullException(nameof(category));

            try
            {
                if (string.IsNullOrWhiteSpace(category.Name))
                    throw new ArgumentException("The category name cannot be null or empty.", nameof(category.Name));

                await _categoryRepository.AddAsync(category);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Validation Error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CategoryService.AddCategoryAsync: {ex.Message}");
                throw new Exception("An error occurred while adding the category.", ex);
            }
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            if (category == null) throw new ArgumentNullException(nameof(category));

            try
            {
                if (string.IsNullOrWhiteSpace(category.Name))
                    throw new ArgumentException("The category name cannot be null or empty.", nameof(category.Name));

                if (category.Id <= 0)
                    throw new ArgumentException("Category ID must be greater than zero.", nameof(category.Id));

                await _categoryRepository.UpdateAsync(category);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Validation Error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CategoryService.UpdateCategoryAsync: {ex.Message}");
                throw new Exception("An error occurred while updating the category.", ex);
            }
        }

        public async Task DeleteCategoryAsync(long id)
        {
            if (id <= 0) throw new ArgumentException("Category ID must be greater than zero.", nameof(id));

            try
            {
                await _categoryRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CategoryService.DeleteCategoryAsync: {ex.Message}");
                throw new Exception("An error occurred while deleting the category.", ex);
            }
        }
    }
}
