using SuperProyecto.Core.Entidades;

namespace SuperProyecto.Core.IServices;

public interface IOrdenService
{
    IEnumerable<Orden> GetOrdenes();
    Orden? DetalleOrden(int id);
    void AltaOrden(Orden orden);
    void PagarOrden(int id);   
}