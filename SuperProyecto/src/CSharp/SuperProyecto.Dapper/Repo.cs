using System.Data;
using Dapper;
using MySqlConnector;
namespace SuperProyecto.Dapper;


public abstract class Repo
{
    protected IDbConnection _conexion { get; private set; }
    public Repo(IDbConnection conexion) => _conexion = conexion;
    public Repo(string conexion) => _conexion = new MySqlConnection(conexion);
}