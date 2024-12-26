using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
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
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("SELECT * FROM Privilege", connection))
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
            catch (SqlException ex)
            {
                // Log SQL-specific errors
                Console.WriteLine($"SQL Error: {ex.Message}");
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
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("INSERT INTO Privilege (Name) VALUES (@Name)", connection))
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
            catch (SqlException ex)
            {
                // Log SQL-specific errors
                Console.WriteLine($"SQL Error: {ex.Message}");
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
