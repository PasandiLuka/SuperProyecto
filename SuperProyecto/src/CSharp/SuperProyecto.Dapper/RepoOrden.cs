using System.Data;
using Dapper;

using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core;
using System.Net.Http.Headers;

namespace SuperProyecto.Dapper;

public class RepoOrden : Repo, IRepoOrden
{
    public RepoOrden(IAdo _ado) : base(_ado) { }


    private static readonly string _queryOrdenes
        = @"SELECT * FROM Orden";
    public IEnumerable<Orden> GetOrdenes() => _conexion.Query<Orden>(_queryOrdenes);

    private static readonly string _queryDetalleOrden
        = @"SELECT * FROM Orden WHERE idOrden = @unIdOrden";
    public Orden? DetalleOrden(int numeroOrden) => _conexion.QueryFirstOrDefault<Orden>(_queryDetalleOrden, new { unIdOrden = numeroOrden });

    private static readonly string _queryAltaOrden
        = @"INSERT INTO Orden (DNI, idFuncion, fecha) VALUES (@DNI, @idFuncion, @fecha)";
    public void AltaOrden(Orden orden)
    {
        _conexion.Execute(
            _queryAltaOrden,
            new
            {
                orden.DNI,
                orden.idFuncion,
                orden.fecha
            });
    }

    private static readonly string _queryPagarOrden
        = @"UPDATE Orden SET pagada = TRUE WHERE idOrden = @unIdOrden";
    public void PagarOrden(int idOrden)
    {
        _conexion.Execute(
            _queryPagarOrden,
            new
            {
                unIdOrden = idOrden
            });
    }
}