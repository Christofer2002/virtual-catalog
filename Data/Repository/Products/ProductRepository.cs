using Npgsql;
using VirtualCatalogAPI.Models.Products;

namespace VirtualCatalogAPI.Data.Repository.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                                ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<List<Product>> GetAllAsync()
        {
            var products = new List<Product>();

            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                using (var command = new NpgsqlCommand("SELECT * FROM Product", connection))
                {
                    await connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            products.Add(new Product
                            {
                                Id = reader["Id"] is DBNull ? 0 : Convert.ToInt64(reader["Id"]),
                                Name = reader["Name"]?.ToString(),
                                Description = reader["Description"]?.ToString(),
                                Price = reader["Price"] is DBNull ? 0 : Convert.ToDecimal(reader["Price"]),
                                Stock = reader["Stock"] is DBNull ? 0 : Convert.ToInt32(reader["Stock"]),
                                CategoryId = reader["CategoryId"] is DBNull ? 0 : Convert.ToInt64(reader["CategoryId"])
                            });
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"PostgreSQL Error: {ex.Message}");
                throw new Exception("An error occurred while retrieving products from the database.", ex);
            }

            return products;
        }

        public async Task<Product> GetProductByIdAsync(long id)
        {
            if (id <= 0) throw new ArgumentException("Product ID must be greater than zero.", nameof(id));

            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                using (var command = new NpgsqlCommand("SELECT * FROM Product WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    await connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Product
                            {
                                Id = reader["Id"] is DBNull ? 0 : Convert.ToInt64(reader["Id"]),
                                Name = reader["Name"]?.ToString(),
                                Description = reader["Description"]?.ToString(),
                                Price = reader["Price"] is DBNull ? 0 : Convert.ToDecimal(reader["Price"]),
                                Stock = reader["Stock"] is DBNull ? 0 : Convert.ToInt32(reader["Stock"]),
                                CategoryId = reader["CategoryId"] is DBNull ? 0 : Convert.ToInt64(reader["CategoryId"])
                            };
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"PostgreSQL Error: {ex.Message}");
                throw new Exception("An error occurred while retrieving the product from the database.", ex);
            }

            return null;
        }

        public async Task AddAsync(Product product)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));

            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                using (var command = new NpgsqlCommand(
                    "INSERT INTO Product (Name, Description, Price, Stock, CategoryId) VALUES (@Name, @Description, @Price, @Stock, @CategoryId)",
                    connection))
                {
                    command.Parameters.AddWithValue("@Name", product.Name.Trim());
                    command.Parameters.AddWithValue("@Description", string.IsNullOrWhiteSpace(product.Description) ? DBNull.Value : product.Description.Trim());
                    command.Parameters.AddWithValue("@Price", product.Price);
                    command.Parameters.AddWithValue("@Stock", product.Stock);
                    command.Parameters.AddWithValue("@CategoryId", product.CategoryId);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"PostgreSQL Error: {ex.Message}");
                throw new Exception("An error occurred while adding the product to the database.", ex);
            }
        }

        public async Task UpdateAsync(Product product)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));

            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                using (var command = new NpgsqlCommand(
                    "UPDATE Product SET Name = @Name, Description = @Description, Price = @Price, Stock = @Stock, CategoryId = @CategoryId WHERE Id = @Id",
                    connection))
                {
                    command.Parameters.AddWithValue("@Id", product.Id);
                    command.Parameters.AddWithValue("@Name", product.Name.Trim());
                    command.Parameters.AddWithValue("@Description", string.IsNullOrWhiteSpace(product.Description) ? DBNull.Value : product.Description.Trim());
                    command.Parameters.AddWithValue("@Price", product.Price);
                    command.Parameters.AddWithValue("@Stock", product.Stock);
                    command.Parameters.AddWithValue("@CategoryId", product.CategoryId);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"PostgreSQL Error: {ex.Message}");
                throw new Exception("An error occurred while updating the product in the database.", ex);
            }
        }

        public async Task DeleteAsync(long id)
        {
            if (id <= 0) throw new ArgumentException("Product ID must be greater than zero.", nameof(id));

            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                using (var command = new NpgsqlCommand("DELETE FROM Product WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"PostgreSQL Error: {ex.Message}");
                throw new Exception("An error occurred while deleting the product from the database.", ex);
            }
        }

        public async Task<List<Product>> GetByCategoryAsync(long categoryId)
        {
            if (categoryId <= 0) throw new ArgumentException("Category ID must be greater than zero.", nameof(categoryId));

            var products = new List<Product>();

            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                using (var command = new NpgsqlCommand("SELECT * FROM Product WHERE CategoryId = @CategoryId", connection))
                {
                    command.Parameters.AddWithValue("@CategoryId", categoryId);
                    await connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            products.Add(new Product
                            {
                                Id = reader["Id"] is DBNull ? 0 : Convert.ToInt64(reader["Id"]),
                                Name = reader["Name"]?.ToString(),
                                Description = reader["Description"]?.ToString(),
                                Price = reader["Price"] is DBNull ? 0 : Convert.ToDecimal(reader["Price"]),
                                Stock = reader["Stock"] is DBNull ? 0 : Convert.ToInt32(reader["Stock"]),
                                CategoryId = reader["CategoryId"] is DBNull ? 0 : Convert.ToInt64(reader["CategoryId"])
                            });
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"PostgreSQL Error: {ex.Message}");
                throw new Exception("An error occurred while retrieving products by category from the database.", ex);
            }

            return products;
        }
    }
}
