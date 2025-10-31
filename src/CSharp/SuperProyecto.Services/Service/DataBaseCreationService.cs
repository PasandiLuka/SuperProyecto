using System.Data;
using System.Linq.Expressions;
using Dapper;
using MySqlConnector;
using SuperProyecto.Core.IServices;

namespace SuperProyecto.Services.Service;

public class DataBaseCreationService
{

    readonly IDataBaseConnectionService _dataBaseConnectionService;
    readonly string connectionString;
    public DataBaseCreationService(IDataBaseConnectionService dataBaseConnectionService)
    {
        _dataBaseConnectionService = dataBaseConnectionService;
        connectionString = _dataBaseConnectionService.GetConnectionRootString();
    }

    public void CreateDataBase()
    {
        var builder = new MySqlConnectionStringBuilder(connectionString);
        string databaseName = builder.Database;
        builder.Database = "";

        using var connection = new MySqlConnection(builder.ConnectionString);

        string schemaDDL = Path.Combine(AppContext.BaseDirectory, "../../../../../../scripts/bd/MySQL/00 DDL.sql");
        string schemaUsers = Path.Combine(AppContext.BaseDirectory, "../../../../../../scripts/bd/MySQL/01 USERS.sql");

        string schemaSql = File.ReadAllText(schemaDDL) + File.ReadAllText(schemaUsers);
        
        try
        {
            if (!DatabaseExists(databaseName))
            {
                using var db = new MySqlConnection(builder.ConnectionString);
                db.Open();
                db.Execute($"CREATE DATABASE IF NOT EXISTS {databaseName}; USE {databaseName}; {schemaSql}");
            }
            else return;
        }
        catch (Exception ex)
        {
            // Si algo explota, limpiamos
            using var cleanConn = new MySqlConnection(builder.ConnectionString);
            cleanConn.Open();
            cleanConn.Execute($"DROP DATABASE `{databaseName}`;");
            
            throw new Exception("Error creando la base, se eliminó por seguridad", ex);
        }
    }
    
    bool DatabaseExists(string databaseName)
    {
        var builder = new MySqlConnectionStringBuilder(connectionString);
        builder.Database = ""; // conectarse sin seleccionar DB

        using var conn = new MySqlConnection(builder.ConnectionString);
        conn.Open();

        var result = conn.QueryFirstOrDefault<string>(
            "SELECT SCHEMA_NAME FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = @db",
            new { db = databaseName }
        );

        return result != null;
    }
}