using System.Data;

namespace SuperProyecto.Dapper;


public abstract class Repo
{
    protected IDbConnection _conexion { get; private set; }
    public Repo(IDbConnection conexion) => _conexion = conexion;
}