using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.DTO;

namespace SuperProyecto.Core.IServices;

public interface IOrdenService
{
    IEnumerable<Orden> GetOrdenes();
    Orden? DetalleOrden(int id);
    void AltaOrden(OrdenDto orden);
    void PagarOrden(int id);   
}