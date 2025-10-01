using System.Data;
using Dapper;

using SuperProyecto.Core.Services.Persistencia;
using SuperProyecto.Core;

namespace SuperProyecto.Dapper;

public class RepoFuncion : Repo, IRepoFuncion
{
    public RepoFuncion(IDbConnection conexion) : base(conexion) { }

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
        = @"INSERT INTO Funcion (idFuncion ,idEvento, descripcion, fechaHora) VALUES (@idFuncion , @idEvento, @descripcion, @fechaHora)";

    public void AltaFuncion(Funcion funcion)
    {
        _conexion.Execute(_queryAltaFuncion, new { funcion.idFuncion, funcion.idEvento, funcion.descripcion, funcion.fechaHora });
    }
    
    private static readonly string _queryUpdateFuncion
        = @"UPDATE Funcion SET idEvento = @unIdEvento, descripcion = @unaDescripcion, fechaHora = @unaFechaHora WHERE idFuncion = @unIdFuncion";
    public void UpdateFuncion(Funcion funcion, int id)
    {
        _conexion.Execute(_queryUpdateFuncion, new { unIdFuncion = id, unIdEvento = funcion.idEvento, unaDescripcion = funcion.descripcion, unaFechaHora = funcion.fechaHora });
    }
}

    
   
    