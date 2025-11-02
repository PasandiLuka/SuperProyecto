using SuperProyecto.Core.Entidades;

namespace SuperProyecto.Core.Persistencia;

public interface IRepoOrden
{
    Orden? DetalleOrdenDeleteSector(int idSector);
    IEnumerable<Orden> GetOrdenes();
    Orden? DetalleOrden(int idOrden);
    void AltaOrden(Orden orden);
    void PagarOrden(int idOrden);
}