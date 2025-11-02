using System.Data;
using MySqlConnector;
using SuperProyecto.Core.IServices;
using SuperProyecto.Core.Persistencia;

namespace SuperProyecto.Dapper;

public class Ado : IAdo
{
    private readonly string _conexion;
    public Ado(IDataBaseConnectionService _service, IGetRolActualService _serviceRol) => _conexion = _service.GetConnectionUserString(_serviceRol.GetRolActual());

    public IDbConnection GetDbConnection()
    {
        return new MySqlConnection(_conexion);
    }    
}