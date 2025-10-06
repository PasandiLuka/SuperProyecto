using System.Data;
using Dapper;

using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core;

namespace SuperProyecto.Dapper;

public class RepoLocal : Repo, IRepoLocal
{
    public RepoLocal(IAdo _ado) : base(_ado) { }

    private static readonly string _queryDetalleLocal =
        @"SELECT * FROM Local WHERE idLocal = @idLocal";
    public Local? DetalleLocal(int IdLocal)
    {
        return _conexion.QueryFirstOrDefault<Local>(_queryDetalleLocal, new { idLocal = IdLocal });
    }

    private static readonly string _queryLocal = "SELECT * FROM Local";
    public IEnumerable<Local> GetLocales() => _conexion.Query<Local>(_queryLocal);

    private static readonly string _queryAltaLocal
        = @"INSERT INTO Local (nombre, direccion) VALUES (@nombre, @direccion)";
    public void AltaLocal(Local local)
    {
        _conexion.Execute(
            _queryAltaLocal,
            new
            {
                local.nombre,
                local.direccion
            });
    }
    
    private static readonly string _queryUpdateLocal
        = @"UPDATE Local SET nombre = @nombre, direccion = @direccion  WHERE idLocal = @idLocal";
    public void UpdateLocal(Local local, int id)
    {
        _conexion.Execute(
            _queryUpdateLocal,
            new
            {
                local.nombre,
                local.direccion,
                idLocal = id
            });
    }

    private static readonly string _queryDeleteLocal
        = @"DELETE FROM Local WHERE idLocal = @idLocal";
    public void DeleteLocal(int IdLocal)
    {
        _conexion.Execute(
            _queryDeleteLocal,
            new
            {
                IdLocal
            });
    }
}