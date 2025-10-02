using System.Data;
using Dapper;

using SuperProyecto.Core.Services.Persistencia;
using SuperProyecto.Core;
using System.Net.Http.Headers;

namespace SuperProyecto.Dapper;

public class RepoOrden : Repo, IRepoOrden
{
    /* public RepoOrden(IDbConnection conexion) : base(conexion) { }
    public RepoOrden(string conexion) : base(conexion) { } */

    public RepoOrden(IAdo _ado) : base(_ado) { }


    private static readonly string _queryOrdenes
        = @"SELECT * FROM Orden";
    public IEnumerable<Orden> GetOrdenes() => _conexion.Query<Orden>(_queryOrdenes);

    private static readonly string _queryDetalleOrden
        = @"SELECT * FROM Orden WHERE idOrden = @unIdOrden";
    public Orden? DetalleOrden(int numeroOrden) => _conexion.QueryFirstOrDefault<Orden>(_queryDetalleOrden, new { unIdOrden = numeroOrden });

    private static readonly string _queryAltaOrden
        = @"INSERT INTO Orden (DNI ,fechaHoraCompra, precioTotal) VALUES (@unDNI, @unaFechaCompra, @unPrecioTotal)";
    public void AltaOrden(Orden orden) => _conexion.Execute(_queryAltaOrden, new { unDNI = orden.DNI, unaFechaCompra = orden.fechaCompra, unPrecioTotal = orden.precioTotal });

    private static readonly string _queryUpdateOrden
        = @"UPDATE Orden SET DNI = @unDNI, fechaCompra = @unaFechaCompra, precioTotal = @unPrecioTotal WHERE numeroOrden = @unNumeroOrden"; 
    public void UpdateOrden(Orden orden, int id) => _conexion.Execute(_queryUpdateOrden, new { unNumeroOrden = id, unDNI = orden.DNI, unaFechaCompra = orden.fechaCompra, unPrecioTotal = orden.precioTotal });
}