using System.Data;
using Dapper;
using MySqlConnector;
using SuperProyecto.Core.Persistencia;
namespace SuperProyecto.Dapper;


public abstract class Repo
{
    protected IDbConnection _conexion;
    public Repo(IAdo _ado) => _conexion = _ado.GetDbConnection();
}