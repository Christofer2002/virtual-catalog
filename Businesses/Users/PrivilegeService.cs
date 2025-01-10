using Npgsql;
using VirtualCatalogAPI.Models.Users;

namespace VirtualCatalogAPI.Data
{
    public class PrivilegeService
    {
        private readonly string _connectionString;

        public PrivilegeService(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public List<Privilege> GetAll()
        {
            var privileges = new List<Privilege>();

            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                using (var command = new NpgsqlCommand("SELECT * FROM Privilege", connection))
                {
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            privileges.Add(new Privilege
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
                // Log PostgreSQL-specific errors
                Console.WriteLine($"PostgreSQL Error: {ex.Message}");
                throw new Exception("An error occurred while retrieving privileges from the database.", ex);
            }
            catch (Exception ex)
            {
                // Log general errors
                Console.WriteLine($"Error: {ex.Message}");
                throw new Exception("An unexpected error occurred.", ex);
            }

            return privileges;
        }

        public void Add(Privilege privilege)
        {
            if (privilege == null) throw new ArgumentNullException(nameof(privilege));

            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                using (var command = new NpgsqlCommand("INSERT INTO Privilege (Name) VALUES (@Name)", connection))
                {
                    // Validate required fields
                    if (string.IsNullOrWhiteSpace(privilege.Name))
                        throw new ArgumentException("Privilege Name is required.", nameof(privilege.Name));

                    // Add parameter
                    command.Parameters.AddWithValue("@Name", privilege.Name.Trim());

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (NpgsqlException ex)
            {
                // Log PostgreSQL-specific errors
                Console.WriteLine($"PostgreSQL Error: {ex.Message}");
                throw new Exception("An error occurred while adding the privilege to the database.", ex);
            }
            catch (ArgumentException ex)
            {
                // Handle validation errors
                Console.WriteLine($"Validation Error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                // Log general errors
                Console.WriteLine($"Error: {ex.Message}");
                throw new Exception("An unexpected error occurred.", ex);
            }
        }
    }
}
