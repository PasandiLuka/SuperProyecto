using MySqlConnector;

namespace SuperProyecto.Services.Service;

public class DataBaseCreationService
{
    public void CreateDataBase(string connectionString, string databaseName)
    {
        var builder = new MySqlConnectionStringBuilder(connectionString);
        var originalDatabase = builder.Database;
        builder.Database = "";

        using var connection = new MySqlConnection(builder.ConnectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = $"CREATE DATABASE IF NOT EXISTS `{databaseName}`;";
        command.ExecuteNonQuery();

        builder.Database = originalDatabase;
    }
}