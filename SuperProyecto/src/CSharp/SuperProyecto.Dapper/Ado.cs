using System.Data;
using MySqlConnector;
using SuperProyecto.Core.Services.Persistencia;

namespace SuperProyecto.Dapper;

public class Ado : IAdo
{
    private readonly string _conexion;
    public Ado(string connection) => _conexion = connection;

    public IDbConnection GetDbConnection()
    {
        return new MySqlConnection(_conexion);
    }    
}