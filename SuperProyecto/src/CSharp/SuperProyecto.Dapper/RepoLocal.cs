using System.Data;
using Dapper;

using SuperProyecto.Core.Services.Persistencia;
using SuperProyecto.Core;

namespace SuperProyecto.Dapper;

public class RepoLocal : Repo, IRepoLocal
{
    public RepoLocal(IDbConnection conexion) : base(conexion) { }

    private static readonly string _queryLocal = "SELECT * FROM Local";
    public IEnumerable<Local> GetLocales() => _conexion.Query<Local>(_queryLocal);

    private static readonly string _queryAltaLocal =
        @"INSERT INTO Local (idLocal, direccion, capacidadMax) VALUES (@idLocal, @direccion, @capacidadMax)";
    public void AltaLocal(Local local)
    {
        _conexion.Execute(_queryAltaLocal, new { local.idLocal, local.direccion, local.capacidadMax });
    }

    private static readonly string _queryDetalleLocal =
        @"SELECT * FROM Local WHERE idLocal = @idLocal";
    public Local? DetalleLocal(int IdLocal)
    {
        return _conexion.QueryFirstOrDefault<Local>(_queryDetalleLocal, new { idLocal = IdLocal });
    }
    
    private static readonly string _queryUpdateLocal
        = @"UPDATE Funcion SET idLocal = @unIdLocal, direccion = @unaDireccion, capacidadMax = @unaCapacidad";
    public void UpdateLocal(Local local, int id)
    {
        _conexion.Execute(_queryUpdateLocal, new { unIdLocal =  local.idLocal, unaDireccion = local.direccion, unaCapacidad = local.capacidadMax});
    }
}