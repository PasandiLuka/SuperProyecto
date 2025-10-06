namespace SuperProyecto.Core.Persistencia;

public interface IRepoOrden
{
    IEnumerable<Orden> GetOrdenes();
    Orden? DetalleOrden(int idOrden);
    void AltaOrden(Orden orden);
    void UpdateOrden(Orden orden, int id);
}