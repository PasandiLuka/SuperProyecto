using SuperProyecto.Core.IServices;
using MySqlConnector;
using Microsoft.Extensions.Configuration;


namespace SuperProyecto.Services.Service;

public class DataBaseConnectionService : IDataBaseConnectionService
{
    public string GetConnectionString()
    {
        // 1) Leer Json
        var configuration = LeerJson();

        // 2) Obtener todas las cadenas de conexión
        var connectionStrings = configuration.GetSection("ConnectionStrings").GetChildren();

        // 3) Probar cada conexión
        string? conectionString = ProbarCadenas(connectionStrings);

        // 4) Resultado final
        if (conectionString == null)
        {
            throw new ArgumentException("⚠ Ninguna cadena de conexión funcionó.");
        }
        else
        {
            Console.WriteLine($"✔ Usando conexión: {conectionString}");
        }

        return conectionString;
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
                Console.WriteLine($"🔄 Probando: {cs.Key}");
                var connection = RemoverDatabaseDeLaCadena(cs.Value);
                if (ProbarConexion(connection))
                {
                    Console.WriteLine($"✅ Conexión válida: {cs.Key}");
                    return cs.Value;
                }
                else
                {
                    Console.WriteLine($"❌ Falló: {cs.Key}");
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