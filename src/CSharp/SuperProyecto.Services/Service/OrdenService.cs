using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.IServices;
using SuperProyecto.Core.DTO;
using SuperProyecto.Services.Validators;
using MySqlConnector;

namespace SuperProyecto.Services.Service;

public class OrdenService : IOrdenService
{
    readonly IRepoOrden _repoOrden;
    readonly IRepoQr _repoQr;
    readonly IQrService _qrService;
    readonly OrdenValidator _validador;

    public OrdenService(IRepoOrden repoOrden, IRepoQr repoQr, IQrService qrService, OrdenValidator validador)
    {
        _repoOrden = repoOrden;
        _repoQr = repoQr;
        _qrService = qrService;
        _validador = validador;
    }

    public Result<IEnumerable<Orden>> GetOrdenes()
    {
        try
        {
            return Result<IEnumerable<Orden>>.Ok(_repoOrden.GetOrdenes());
        }
        catch (MySqlException)
        {
            return Result<IEnumerable<Orden>>.Unauthorized();
        }
    }

    public Result<Orden?> DetalleOrden(int id)
    {
        try
        {
            if (_repoOrden.DetalleOrden(id) is null) return Result<Orden?>.NotFound("La orden solicitada no fue encontrada.");
            return Result<Orden?>.Ok(_repoOrden.DetalleOrden(id));
        }
        catch (MySqlException)
        {
            return Result<Orden>.Unauthorized();
        }
    }

    public Result<OrdenDto> AltaOrden(OrdenDto ordenDto)
    {
        try
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
        catch (MySqlException)
        {
            return Result<OrdenDto>.Unauthorized();
        }
    }

    public Result<Orden> PagarOrden(int id)
    {
        try
        {
            if(_repoOrden.DetalleOrden(id) is null) return Result<Orden>.NotFound("La orden a pagar no fue encontrada.");
            _repoOrden.PagarOrden(id);
            var url = _qrService.GenerarQrUrl(id);
            _repoQr.AltaQr(id, url);
            return Result<Orden>.Ok();
        }
        catch (MySqlException)
        {
            return Result<Orden>.Unauthorized();
        }
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