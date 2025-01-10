using System.Data;
using Npgsql;

namespace VirtualCatalogAPI.Data.Connection;
public class DatabaseHelper
{
    private readonly string _connectionString;

    public DatabaseHelper(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public DataTable ExecuteQuery(string query, NpgsqlParameter[] parameters = null)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        using (var command = new NpgsqlCommand(query, connection))
        {
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }

            var dataTable = new DataTable();
            var adapter = new NpgsqlDataAdapter(command);

            connection.Open();
            adapter.Fill(dataTable);
            return dataTable;
        }
    }

    public int ExecuteNonQuery(string query, NpgsqlParameter[] parameters = null)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        using (var command = new NpgsqlCommand(query, connection))
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
