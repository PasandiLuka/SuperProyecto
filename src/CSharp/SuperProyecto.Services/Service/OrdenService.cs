using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.IServices;
using SuperProyecto.Core.DTO;

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

    public void AltaOrden(OrdenDto ordenDto)
    {
        Orden orden = ConvertirDtoClase(ordenDto);
        _repoOrden.AltaOrden(orden);
    }

    public void PagarOrden(int id) => _repoOrden.PagarOrden(id);


    static Orden ConvertirDtoClase(OrdenDto ordenDto)
    {
        return new Orden
        {
            DNI = ordenDto.DNI,
            idFuncion = ordenDto.idFuncion,
            fecha = DateTime.Now,
            pagada = false
        };
    }
}