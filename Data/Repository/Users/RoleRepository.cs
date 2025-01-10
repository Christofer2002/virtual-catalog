using Npgsql;
using System.Xml.Linq;
using VirtualCatalogAPI.Models.Users;

namespace VirtualCatalogAPI.Data.Repository.Users
{
    public class RoleRepository : IRoleRepository
    {
        private readonly string _connectionString;

        public RoleRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<List<Role>> GetAllRolesAsync()
        {
            var roles = new List<Role>();
            const string query = "SELECT * FROM \"Role\"";

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
                            roles.Add(new Role
                            {
                                Id = Convert.ToInt64(reader["Id"]),
                                Name = reader["Name"]?.ToString()
                            });
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"PostgreSQL Error in RoleRepository.GetAllRolesAsync: {ex.Message}");
                throw new Exception("An error occurred while retrieving roles from the database.", ex);
            }

            return roles;
        }

        public async Task<Role> GetRoleByIdAsync(long id)
        {
            if (id <= 0)
                throw new ArgumentException("Id must be greater than zero.", nameof(id));

            const string query = "SELECT * FROM \"Role\" WHERE \"Id\" = @Id";

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
                            return new Role
                            {
                                Id = Convert.ToInt64(reader["Id"]),
                                Name = reader["Name"]?.ToString()
                            };
                        }
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"PostgreSQL Error in RoleRepository.GetRoleByIdAsync: {ex.Message}");
                throw new Exception("An error occurred while retrieving the role by ID from the database.", ex);
            }

            return null;
        }

        public async Task<long> CreateRoleAsync(Role role)
        {
            if (role == null || string.IsNullOrWhiteSpace(role.Name))
                throw new ArgumentException("Role name cannot be null or empty.", nameof(role));

            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                using (var command = new NpgsqlCommand("INSERT INTO \"Role\" (\"Name\") VALUES(@Name) RETURNING \"Id\";", connection))
                {
                    command.Parameters.AddWithValue("@Name", role.Name);
                    await connection.OpenAsync();

                    return (long)await command.ExecuteScalarAsync();
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"PostgreSQL Error in RoleRepository.CreateRoleAsync: {ex.Message}");
                throw new Exception("An error occurred while creating the role in the database.", ex);
            }
        }

        public async Task UpdateRoleAsync(Role role)
        {
            if (role == null || string.IsNullOrWhiteSpace(role.Name) || role.Id <= 0)
                throw new ArgumentException("Invalid role data.", nameof(role));

            const string query = "UPDATE \"Role\" SET \"Name\" = @Name WHERE \"Id\" = @Id";

            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", role.Name);
                    command.Parameters.AddWithValue("@Id", role.Id);
                    await connection.OpenAsync();

                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"PostgreSQL Error in RoleRepository.UpdateRoleAsync: {ex.Message}");
                throw new Exception("An error occurred while updating the role in the database.", ex);
            }
        }

        public async Task DeleteRoleAsync(long id)
        {
            if (id <= 0)
                throw new ArgumentException("Id must be greater than zero.", nameof(id));

            const string query = "DELETE FROM \"Role\" WHERE \"Id\" = @Id";

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
                Console.WriteLine($"PostgreSQL Error in RoleRepository.DeleteRoleAsync: {ex.Message}");
                throw new Exception("An error occurred while deleting the role from the database.", ex);
            }
        }
    }
}
