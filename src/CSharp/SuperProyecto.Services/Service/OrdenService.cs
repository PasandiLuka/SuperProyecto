using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.IServices;

namespace SuperProyecto.Services.Service;

public class OrdenService : IOrdenService
{
    readonly IRepoOrden _repoOrden;

    public OrdenService(IRepoOrden repoOrden)
    {
        _repoOrden = repoOrden;
    }

    public IEnumerable<Orden> GetOrdenes() => _repoOrden.GetOrdenes();

    public Orden? DetalleOrden(int id) => _repoOrden.DetalleOrden(id);

    public void AltaOrden(Orden orden) => _repoOrden.AltaOrden(orden);

    public void PagarOrden(int id) => _repoOrden.PagarOrden(id);
}