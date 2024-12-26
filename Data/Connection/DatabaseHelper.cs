using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace VirtualCatalogAPI.Data
{
    public class DatabaseHelper
    {
        private readonly string _connectionString;

        public DatabaseHelper(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                var dataTable = new DataTable();
                var adapter = new SqlDataAdapter(command);

                connection.Open();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }

        public int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                connection.Open();
                return command.ExecuteNonQuery();
            }
        }
    }
}
