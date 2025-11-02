using SuperProyecto.Core.IServices;
using MySqlConnector;
using Microsoft.Extensions.Configuration;
using SuperProyecto.Core.Enums;


namespace SuperProyecto.Services.Service;

public class DataBaseConnectionService : IDataBaseConnectionService
{
    public string GetConnectionRootString()
    {
        // 1) Leer Json
        var configuration = LeerJson();

        // 2) Obtener todas las cadenas de conexión
        var connectionStrings = configuration.GetSection("Root").GetChildren();

        // 3) Probar cada conexión
        string? conectionString = ProbarCadenas(connectionStrings);

        // 4) Resultado final
        if (conectionString == null)
        {
            throw new ArgumentException("Ninguna cadena de conexión root funcionó.");
        }
        
        return conectionString;
    }

    public string GetConnectionUserString(string rol)
    {
        var configuration = LeerJson();

        var connectionStrings = configuration.GetSection("Users").GetChildren();

        return connectionStrings.First(c => c.Key == rol).Value!;
    }
    
    static IConfigurationRoot LeerJson()
    {
        // 1) Leer appsettings.json
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(
                "appsettings.json",
                optional: false
                )
            .Build();
        return configuration;
    }

    static bool ProbarConexion(string cs)
    {
        try
        {
            using var conn = new MySqlConnection(cs);
            conn.Open();
            return true;
        }
        catch
        {
            return false;
        }
    }

    static string RemoverDatabaseDeLaCadena(string cs)
    {
        var builder = new MySqlConnectionStringBuilder(cs);
        builder.Remove("Database");
        return builder.ConnectionString;
    }

    static string ProbarCadenas(IEnumerable<IConfigurationSection>? connectionStrings)
    {
        if (connectionStrings != null)
        {
            foreach (var cs in connectionStrings)
            {
                var connection = RemoverDatabaseDeLaCadena(cs.Value);
                if (ProbarConexion(connection))
                {
                    return cs.Value;
                }
            }
            return null;
        }
        else
        {
            return null;
        }
    }
}