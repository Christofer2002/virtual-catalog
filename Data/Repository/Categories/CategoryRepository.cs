using Npgsql;
using System.Threading.Tasks;
using VirtualCatalogAPI.Models.Categories;

namespace VirtualCatalogAPI.Data.Repository.Categories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly string _connectionString;

        public CategoryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<List<Category>> GetAllAsync()
        {
            var categories = new List<Category>();
            const string query = "SELECT * FROM \"Category\"";

            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                using (var command = new NpgsqlCommand(query, connection))
                {
                    await connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            categories.Add(new Category
                            {
                                Id = reader["Id"] is DBNull ? 0 : Convert.ToInt64(reader["Id"]),
                                Name = reader["Name"]?.ToString()
                            });
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"PostgreSQL Error in GetAllAsync: {ex.Message}");
                throw new Exception("An error occurred while retrieving categories from the database.", ex);
            }

            return categories;
        }

        public async Task<Category> GetByIdAsync(long id)
        {
            if (id <= 0) throw new ArgumentException("Category ID must be greater than zero.", nameof(id));

            const string query = "SELECT * FROM \"Category\" WHERE \"Id\" = @Id";

            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    await connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Category
                            {
                                Id = reader["Id"] is DBNull ? 0 : Convert.ToInt64(reader["Id"]),
                                Name = reader["Name"]?.ToString()
                            };
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"PostgreSQL Error in GetByIdAsync: {ex.Message}");
                throw new Exception("An error occurred while retrieving the category by ID from the database.", ex);
            }

            return null;
        }

        public async Task AddAsync(Category category)
        {
            if (category == null) throw new ArgumentNullException(nameof(category));

            const string query = "INSERT INTO \"Category\" (\"Name\") VALUES (@Name)";

            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                using (var command = new NpgsqlCommand(query, connection))
                {
                    if (string.IsNullOrWhiteSpace(category.Name))
                        throw new ArgumentException("Category Name is required.", nameof(category.Name));

                    command.Parameters.AddWithValue("@Name", category.Name.Trim());

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"PostgreSQL Error in AddAsync: {ex.Message}");
                throw new Exception("An error occurred while adding the category to the database.", ex);
            }
        }

        public async Task UpdateAsync(Category category)
        {
            if (category == null) throw new ArgumentNullException(nameof(category));

            const string query = "UPDATE \"Category\" SET \"Name\" = @Name WHERE \"Id\" = @Id";

            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                using (var command = new NpgsqlCommand(query, connection))
                {
                    if (string.IsNullOrWhiteSpace(category.Name))
                        throw new ArgumentException("Category Name is required.", nameof(category.Name));

                    if (category.Id <= 0)
                        throw new ArgumentException("Category ID must be greater than zero.", nameof(category.Id));

                    command.Parameters.AddWithValue("@Id", category.Id);
                    command.Parameters.AddWithValue("@Name", category.Name.Trim());

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"PostgreSQL Error in UpdateAsync: {ex.Message}");
                throw new Exception("An error occurred while updating the category in the database.", ex);
            }
        }

        public async Task DeleteAsync(long id)
        {
            if (id <= 0) throw new ArgumentException("Category ID must be greater than zero.", nameof(id));

            const string query = "DELETE FROM \"Category\" WHERE \"Id\" = @Id";

            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"PostgreSQL Error in DeleteAsync: {ex.Message}");
                throw new Exception("An error occurred while deleting the category from the database.", ex);
            }
        }
    }
}
