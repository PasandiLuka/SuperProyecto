using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.IServices;
using SuperProyecto.Core.DTO;
using SuperProyecto.Services.Validators;

namespace SuperProyecto.Services.Service;

public class OrdenService : IOrdenService
{
    readonly IRepoOrden _repoOrden;
    readonly OrdenValidator _validador;

    public OrdenService(IRepoOrden repoOrden, OrdenValidator validador)
    {
        _repoOrden = repoOrden;
        _validador = validador;
    }

    public Result<IEnumerable<Orden>> GetOrdenes() => Result<IEnumerable<Orden>>.Ok(_repoOrden.GetOrdenes());

    public Result<Orden?> DetalleOrden(int id)
    {
        if (_repoOrden.DetalleOrden(id) is null) return Result<Orden?>.NotFound("La orden solicitada no fue encontrada.");
        return Result<Orden?>.Ok(_repoOrden.DetalleOrden(id));
    }

    public Result<OrdenDto> AltaOrden(OrdenDto ordenDto)
    {
        var resultado = _validador.Validate(ordenDto);
        if (!resultado.IsValid)
        {
            var listaErrores = resultado.Errors
                .GroupBy(a => a.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );
            return Result<OrdenDto>.BadRequest(listaErrores);
        }
        Orden orden = ConvertirDtoClase(ordenDto);
        _repoOrden.AltaOrden(orden);
        return Result<OrdenDto>.Ok(ordenDto);
    }

    public Result<Orden> PagarOrden(int id)
    {
        if(_repoOrden.DetalleOrden(id) is null) return Result<Orden>.NotFound("La orden a pagar no fue encontrada.");
        _repoOrden.PagarOrden(id);
        return Result<Orden>.Ok();
    }


    static Orden ConvertirDtoClase(OrdenDto ordenDto)
    {
        return new Orden
        {
            DNI = ordenDto.DNI,
            idSector = ordenDto.idSector,
            fecha = DateTime.Now,
            pagada = false
        };
    }
}