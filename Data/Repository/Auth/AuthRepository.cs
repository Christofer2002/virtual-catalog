using Npgsql;
using VirtualCatalogAPI.Models.Users;
using Dapper;

namespace VirtualCatalogAPI.Data.Repository.Auth
{
    public class AuthRepository : IAuthRepository
    {
        private readonly string _connectionString;

        public AuthRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                                ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            const string query = @"
            SELECT u.""Id"", u.""Name"", u.""LastName"", u.""Email"", u.""Identification"", u.""Password"", 
                   r.""Name"" AS RoleName
            FROM ""User"" u
            LEFT JOIN ""UserRole"" ur ON u.""Id"" = ur.""UserId""
            LEFT JOIN ""Role"" r ON ur.""RoleId"" = r.""Id""
            WHERE u.""Email"" = @Email";

            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    connection.Open();

                    var user = await connection.QueryFirstOrDefaultAsync<User>(
                        query,
                        new { Email = email }
                    );

                    return user;
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"PostgreSQL Error: {ex.Message}");
                throw new Exception("An error occurred while fetching the user from the database.", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw new Exception("An unexpected error occurred.", ex);
            }
        }

        public async Task CreateUserAsync(User user, int roleId)
        {
            const string insertUserQuery = @"
            INSERT INTO ""User"" (""Name"", ""LastName"", ""Email"", ""Identification"", ""Password"")
            VALUES (@Name, @LastName, @Email, @Identification, @Password)
            RETURNING ""Id"";";

            const string insertUserRoleQuery = @"
            INSERT INTO ""UserRole"" (""UserId"", ""RoleId"")
            VALUES (@UserId, @RoleId);";

            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    connection.Open();

                    // Insert user and get the generated UserId
                    user.Id = await connection.ExecuteScalarAsync<long>(insertUserQuery, user);

                    // Insert into UserRole
                    var userRoleParams = new
                    {
                        UserId = user.Id,
                        RoleId = roleId
                    };

                    await connection.ExecuteAsync(insertUserRoleQuery, userRoleParams);
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"PostgreSQL Error: {ex.Message}");
                throw new Exception("An error occurred while registering the user in the database.", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw new Exception("An unexpected error occurred.", ex);
            }
        }
    }
}
