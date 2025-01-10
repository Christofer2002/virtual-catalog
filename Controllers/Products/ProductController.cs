using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VirtualCatalogAPI.Businesses.Products;
using VirtualCatalogAPI.Models.Products;

namespace VirtualCatalogAPI.Controllers.Products
{
    [ApiController]
    [Route("product")]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        /// <summary>
        /// Get all products.
        /// </summary>
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var products = await _productService.GetAllProductsAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Get products by category ID.
        /// </summary>
        [AllowAnonymous]
        [HttpGet("category/{categoryId:long}")]
        public async Task<IActionResult> GetByCategory(long categoryId)
        {
            try
            {
                var products = await _productService.GetProductsByCategoryAsync(categoryId);
                if (products == null || !products.Any())
                    return NotFound($"No products found for category ID {categoryId}.");

                return Ok(products);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Add a new product.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Product product)
        {
            if (product == null)
            {
                Console.WriteLine("Product object is null.");
                return BadRequest("Product object is null.");
            }
            if (!ModelState.IsValid)
            {
                Console.WriteLine("Invalid product object.");
                return BadRequest(ModelState);
            }

            try
            {
                Console.WriteLine($"Adding product: {product.Name}, CategoryId: {product.CategoryId}");
                await _productService.AddProductAsync(product);
                return CreatedAtAction(nameof(GetByCategory), new { categoryId = product.CategoryId }, product);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Update an existing product.
        /// </summary>
        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, [FromBody] Product product)
        {
            if (product == null) return BadRequest("Product object is null.");
            if (id != product.Id) return BadRequest("Product ID mismatch.");
            if (!ModelState.IsValid) return BadRequest("Invalid product object.");

            try
            {
                await _productService.UpdateProductAsync(product);
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Delete a product by ID.
        /// </summary>
        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                await _productService.DeleteProductAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Get a category by its ID.
        /// </summary>
        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetProductById(long id)
        {
            try
            {
                if (id <= 0) return BadRequest("Invalid Product ID.");

                var Product = await _productService.GetProductByIdAsync(id);
                if (Product == null) return NotFound("Product not found.");

                return Ok(Product); // HTTP 200
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "An error occurred while retrieving the product.");
            }
        }

    }
}
