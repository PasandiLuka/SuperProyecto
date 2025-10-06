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
        = @"INSERT INTO Orden (DNI, fecha, total) VALUES (@DNI, @fecha, @total)";
    public void AltaOrden(Orden orden)
    {
        _conexion.Execute(
            _queryAltaOrden,
            new
            {
                orden.DNI,
                orden.fecha,
                orden.total
            });
    } 

    private static readonly string _queryUpdateOrden
        = @"UPDATE Orden SET DNI = @DNI, fecha  = @fecha, total = @total WHERE idOrden = @idOrden";
    public void UpdateOrden(Orden orden, int id)
    {
        _conexion.Execute(
            _queryUpdateOrden,
            new
            {
                orden.DNI,
                orden.fecha,
                orden.total,
                idOrden = id
            });
    } 
}