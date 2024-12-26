using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using VirtualCatalogAPI.Models.Users;

namespace VirtualCatalogAPI.Data.Repository.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<List<User>> GetAllAsync()
        {
            var users = new List<User>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand(@"
                    SELECT u.Id, u.Name, u.LastName, u.Email, u.Identification, u.Password,
                           r.Name AS RoleName
                    FROM [User] u
                    LEFT JOIN [UserRole] ur ON u.Id = ur.UserId
                    LEFT JOIN [Role] r ON ur.RoleId = r.Id", connection))
                {
                    await connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        var userDictionary = new Dictionary<long, User>();

                        while (await reader.ReadAsync())
                        {
                            var userId = reader["Id"] is DBNull ? 0 : Convert.ToInt64(reader["Id"]);

                            if (!userDictionary.TryGetValue(userId, out var user))
                            {
                                user = new User
                                {
                                    Id = userId,
                                    Name = reader["Name"]?.ToString(),
                                    LastName = reader["LastName"]?.ToString(),
                                    Email = reader["Email"]?.ToString(),
                                    Identification = reader["Identification"]?.ToString(),
                                    Password = reader["Password"]?.ToString(),
                                    UserRoles = new List<UserRole>()
                                };

                                userDictionary[userId] = user;
                            }

                            if (reader["RoleName"] != DBNull.Value)
                            {
                                var roleName = reader["RoleName"].ToString();
                                user.UserRoles.Add(new UserRole
                                {
                                    Role = new Role { Name = roleName }
                                });
                            }
                        }

                        users = userDictionary.Values.ToList();
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
                throw new Exception("An error occurred while retrieving users from the database.", ex);
            }

            return users;
        }

        public async Task<User> GetByIdAsync(long id)
        {
            if (id <= 0)
                throw new ArgumentException("Id must be greater than zero.", nameof(id));

            const string query = @"
                SELECT u.Id, u.Name, u.LastName, u.Email, u.Identification, u.Password, 
                       r.Name AS RoleName
                FROM [User] u
                LEFT JOIN [UserRole] ur ON u.Id = ur.UserId
                LEFT JOIN [Role] r ON ur.RoleId = r.Id
                WHERE u.Id = @Id";

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    await connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new User
                            {
                                Id = reader["Id"] is DBNull ? 0 : Convert.ToInt64(reader["Id"]),
                                Name = reader["Name"]?.ToString(),
                                LastName = reader["LastName"]?.ToString(),
                                Email = reader["Email"]?.ToString(),
                                Identification = reader["Identification"]?.ToString(),
                                Password = reader["Password"]?.ToString(),
                                RoleName = reader["RoleName"]?.ToString() // Map RoleName
                            };
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
                throw new Exception("An error occurred while retrieving the user by ID from the database.", ex);
            }

            return null;
        }

        public async Task<User> GetByIdentificationAsync(string identification)
        {
            if (string.IsNullOrWhiteSpace(identification))
                throw new ArgumentException("Identification is required.", nameof(identification));

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("SELECT * FROM [User] WHERE Identification = @Identification", connection))
                {
                    command.Parameters.AddWithValue("@Identification", identification.Trim());
                    await connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new User
                            {
                                Id = reader["Id"] is DBNull ? 0 : Convert.ToInt64(reader["Id"]),
                                Name = reader["Name"]?.ToString(),
                                LastName = reader["LastName"]?.ToString(),
                                Email = reader["Email"]?.ToString(),
                                Identification = reader["Identification"]?.ToString(),
                                Password = reader["Password"]?.ToString()
                            };
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
                throw new Exception("An error occurred while retrieving the user by identification from the database.", ex);
            }

            return null;
        }

        public async Task AddAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("INSERT INTO [User] (Name, LastName, Email, Identification, Password) VALUES (@Name, @LastName, @Email, @Identification, @Password)", connection))
                {
                    command.Parameters.AddWithValue("@Name", user.Name.Trim());
                    command.Parameters.AddWithValue("@LastName", user.LastName.Trim());
                    command.Parameters.AddWithValue("@Email", user.Email.Trim());
                    command.Parameters.AddWithValue("@Identification", string.IsNullOrWhiteSpace(user.Identification) ? DBNull.Value : (object)user.Identification.Trim());
                    command.Parameters.AddWithValue("@Password", user.Password.Trim());

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
                throw new Exception("An error occurred while adding the user to the database.", ex);
            }
        }

        public async Task UpdateAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("UPDATE [User] SET Name = @Name, LastName = @LastName, Email = @Email, Identification = @Identification, Password = @Password WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", user.Id);
                    command.Parameters.AddWithValue("@Name", user.Name.Trim());
                    command.Parameters.AddWithValue("@LastName", user.LastName.Trim());
                    command.Parameters.AddWithValue("@Email", user.Email.Trim());
                    command.Parameters.AddWithValue("@Identification", string.IsNullOrWhiteSpace(user.Identification) ? DBNull.Value : (object)user.Identification.Trim());
                    command.Parameters.AddWithValue("@Password", user.Password.Trim());

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
                throw new Exception("An error occurred while updating the user in the database.", ex);
            }
        }

        public async Task DeleteAsync(long id)
        {
            if (id <= 0)
                throw new ArgumentException("User ID must be greater than zero.", nameof(id));

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("DELETE FROM [User] WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
                throw new Exception("An error occurred while deleting the user from the database.", ex);
            }
        }
    }
}
