using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.DTO;

namespace SuperProyecto.Core.IServices;

public interface IOrdenService
{
    Result<IEnumerable<Orden>> GetOrdenes();
    Result<Orden?> DetalleOrden(int id);
    Result<OrdenDto> AltaOrden(OrdenDto orden);
    Result<Orden> PagarOrden(int id);   
}