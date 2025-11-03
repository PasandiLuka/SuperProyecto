using System.Diagnostics;
using SuperProyecto.Core.Entidades;

namespace SuperProyecto.Core.Persistencia;

public interface IRepoOrden
{
    IEnumerable<Orden> GetOrdenes();
    Orden? DetalleOrden(int idOrden);
    void AltaOrden(Orden orden);
    void PagarOrden(int idOrden);
    void CancelarOrden(int idOrden);
    void AgregarPrecio(int idOrden, decimal precio);
    void RestarPrecio(int idOrden, decimal precio);
}