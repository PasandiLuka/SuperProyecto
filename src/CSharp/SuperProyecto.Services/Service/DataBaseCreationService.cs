using MySqlConnector;
using SuperProyecto.Core.IServices;

namespace SuperProyecto.Services.Service;

public class DataBaseCreationService
{   
    readonly IDataBaseConnectionService _dataBaseConnectionService;
    public DataBaseCreationService(IDataBaseConnectionService dataBaseConnectionService)
    {
        _dataBaseConnectionService = dataBaseConnectionService;
    }
    
    public void CreateDataBase()
    {
        string connectionString = _dataBaseConnectionService.GetConnectionString();

        var builder = new MySqlConnectionStringBuilder(connectionString);
        var originalDatabase = builder.Database;
        string databaseName = originalDatabase;
        builder.Database = "";

        using var connection = new MySqlConnection(builder.ConnectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = $"CREATE DATABASE IF NOT EXISTS `{databaseName}`;";
        command.ExecuteNonQuery();
    }
}