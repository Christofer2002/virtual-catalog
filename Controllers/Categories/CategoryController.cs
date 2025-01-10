using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VirtualCatalogAPI.Businesses.Categories;
using VirtualCatalogAPI.Models.Categories;

namespace VirtualCatalogAPI.Controllers
{
    [ApiController]
    [Route("category")]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        }

        /// <summary>
        /// Get all categories.
        /// </summary>
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var categories = await _categoryService.GetAllCategoriesAsync();
                return Ok(categories); // HTTP 200
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "An error occurred while retrieving categories.");
            }
        }

        /// <summary>
        /// Get a category by its ID.
        /// </summary>
        [AllowAnonymous]
        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                if (id <= 0) return BadRequest("Invalid Category ID.");

                var category = await _categoryService.GetCategoryByIdAsync(id);
                if (category == null) return NotFound("Category not found.");

                return Ok(category); // HTTP 200
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "An error occurred while retrieving the category.");
            }
        }

        /// <summary>
        /// Add a new category.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Add(Category category)
        {
            try
            {
                if (category == null || string.IsNullOrWhiteSpace(category.Name))
                    return BadRequest("Category name is required.");

                await _categoryService.AddCategoryAsync(category);
                return CreatedAtAction(nameof(GetById), new { id = category.Id }, category); // HTTP 201
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Validation Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "An error occurred while adding the category.");
            }
        }

        /// <summary>
        /// Update an existing category.
        /// </summary>
        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, Category category)
        {
            try
            {
                if (id <= 0 || category == null || id != category.Id)
                    return BadRequest("Invalid category data.");

                await _categoryService.UpdateCategoryAsync(category);
                return NoContent(); // HTTP 204
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Validation Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "An error occurred while updating the category.");
            }
        }

        /// <summary>
        /// Delete a category.
        /// </summary>
        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                if (id <= 0) return BadRequest("Invalid Category ID.");

                await _categoryService.DeleteCategoryAsync(id);
                return NoContent(); // HTTP 204
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Validation Error: {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "An error occurred while deleting the category.");
            }
        }
    }
}
