using System.Data;
using Dapper;

using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core;

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
        = @"INSERT INTO Funcion (idTarifa, idEvento, fechaHora, stock) VALUES (@idTarifa, @idEvento, @fechaHora, @stock)";
    public void AltaFuncion(Funcion funcion)
    {
        _conexion.Execute(
            _queryAltaFuncion,
            new
            {
                funcion.idTarifa,
                funcion.idEvento,
                funcion.fechaHora,
                funcion.stock
            });
    }
    
    private static readonly string _queryUpdateFuncion
        = @"UPDATE Funcion SET idTarifa = @idTarifa, idEvento = @idEvento, fechaHora = @fechaHora, stock = @stock WHERE idFuncion = @unIdFuncion";
    public void UpdateFuncion(Funcion funcion, int id)
    {
        _conexion.Execute(
            _queryUpdateFuncion,
            new
            {
                funcion.idTarifa,
                funcion.idEvento,
                funcion.fechaHora,
                funcion.stock,
                unIdFuncion = id
            });
    }
}