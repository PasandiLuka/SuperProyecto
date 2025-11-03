using Dapper;

using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.Entidades;

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
        = @"INSERT INTO Orden (idCliente, fecha) VALUES (@idCliente, @fecha)";
    public void AltaOrden(Orden orden)
    {
        _conexion.Execute(
            _queryAltaOrden,
            new
            {
                orden.idCliente,
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

    private static readonly string _queryCancelarOrden
        = @"UPDATE Orden SET cancelada = TRUE WHERE idOrden = @unIdOrden";
    public void CancelarOrden(int idOrden)
    {
        _conexion.Execute(
            _queryCancelarOrden,
            new
            {
                unIdOrden = idOrden
            });
    }

    private static readonly string _queryAgregarPrecio
        = @"UPDATE Orden SET total = total + @total WHERE idOrden = @idOrden";
    public void AgregarPrecio(int idOrden, decimal precio)
    {
        _conexion.Execute(
            _queryAgregarPrecio,
            new
            {
                total = precio,
                idOrden
            }
        );
    }

    private static readonly string _queryRestarPrecio
        = @"UPDATE Orden SET total = total - @total WHERE idOrden = @idOrden";
    public void RestarPrecio(int idOrden, decimal precio)
    {
        _conexion.Execute(
            _queryRestarPrecio,
            new
            {
                total = precio,
                idOrden
            }
        );
    }
}