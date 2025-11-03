using Dapper;

using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.Entidades;

namespace SuperProyecto.Dapper;

public class RepoFuncion : Repo, IRepoFuncion
{
    public RepoFuncion(IAdo _ado) : base(_ado) { }
    
    private static readonly string _queryFuncion
        = "SELECT * FROM Funcion";
    public IEnumerable<Funcion> GetFunciones() => _conexion.Query<Funcion>(_queryFuncion);
    
    private static readonly string _queryDetalleFuncion =
        @"SELECT * FROM Funcion WHERE idFuncion = @idFuncion";
    public Funcion? DetalleFuncion(int IdFuncion)
    {
        return _conexion.QueryFirstOrDefault<Funcion>(_queryDetalleFuncion, new { idFuncion = IdFuncion });
    }

    private static readonly string _queryAltaFuncion
        = @"INSERT INTO Funcion (idEvento, idLocal, fechaHora) VALUES ( @idEvento, @idLocal, @fechaHora)";
    public void AltaFuncion(Funcion funcion)
    {
        _conexion.Execute(
            _queryAltaFuncion,
            new
            {
                funcion.idEvento,
                funcion.idLocal,
                funcion.fechaHora
            });
    }
    
    private static readonly string _queryUpdateFuncion
        = @"UPDATE Funcion SET idEvento = @idEvento, idLocal = @idLocal, fechaHora = @fechaHora WHERE idFuncion = @unIdFuncion";
    public void UpdateFuncion(Funcion funcion, int id)
    {
        _conexion.Execute(
            _queryUpdateFuncion,
            new
            {
                funcion.idEvento,
                funcion.idLocal,
                funcion.fechaHora,
                unIdFuncion = id
            });
    }

    private static readonly string _queryCancelarFuncion
        = @"UPDATE Funcion SET cancelada = true WHERE idFuncion = @idFuncion";
    public void CancelarFuncion(int idFuncion)
    {
        _conexion.Execute(
            _queryCancelarFuncion,
            new
            {
                idFuncion
            }
        );   
    }
}