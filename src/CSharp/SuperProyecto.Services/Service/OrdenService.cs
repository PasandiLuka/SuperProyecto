using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.IServices;
using SuperProyecto.Core.DTO;
using SuperProyecto.Services.Validators;
using MySqlConnector;
using System.Data;

namespace SuperProyecto.Services.Service;

public class OrdenService : IOrdenService
{
    readonly IRepoOrden _repoOrden;
    readonly IRepoTarifa _repoTarifa;
    readonly IRepoQr _repoQr;
    readonly IQrService _qrService;
    readonly IRepoEntrada _repoEntrada;
    readonly OrdenValidator _validador;

    public OrdenService(IRepoTarifa repoTarifa, IRepoOrden repoOrden, IRepoQr repoQr, IQrService qrService, IRepoEntrada repoEntrada, OrdenValidator validador)
    {
        _repoTarifa = repoTarifa;
        _repoOrden = repoOrden;
        _repoQr = repoQr;
        _qrService = qrService;
        _repoEntrada = repoEntrada;
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
            if (_repoOrden.DetalleOrden(id) is null) return Result<Orden>.NotFound("La orden a pagar no fue encontrada.");
            if (_repoOrden.DetalleOrden(id).pagada) return Result<Orden>.NotFound("La orden a pagar ya fue pagada.");
            var entradas = _repoEntrada.GetEntradasXOrden(id);
            if (entradas is null) return Result<Orden>.NotFound("La orden a no posee entradas.");
            _repoOrden.PagarOrden(id);
            foreach(var entrada in entradas)
            {
                var url = _qrService.GenerarQrUrl(entrada.idEntrada);
                _repoQr.AltaQr(entrada.idEntrada, url);
            }
            return Result<Orden>.Ok();
        }
        catch (MySqlException)
        {
            return Result<Orden>.Unauthorized();
        }
    }
    
    public Result<Orden> CancelarOrden(int idOrden)
    {
        try
        {
            if (_repoOrden.DetalleOrden(idOrden) is null) return Result<Orden>.NotFound("La orden a cancelar no fue encontrada.");
            if (_repoOrden.DetalleOrden(idOrden).pagada) return Result<Orden>.NotFound("La orden ya fue pagada.");
            if (_repoOrden.DetalleOrden(idOrden).cancelada) return Result<Orden>.NotFound("La orden a cancelar ya fue cancelada.");
            _repoOrden.CancelarOrden(idOrden);
            var entradas = _repoEntrada.GetEntradasXOrden(idOrden);
            foreach (var entrada in entradas)
            {
                _repoEntrada.DevolverStock(entrada.idTarifa);
            }
            return Result<Orden>.Ok();
        }
        catch (MySqlException)
        {
            return Result<Orden>.Unauthorized();
        }
    }

    public Result<Orden> CrearEntrada(int idOrden, int idTarifa)
    {
        try
        {
            var orden = _repoOrden.DetalleOrden(idOrden);
            if (orden is null) Result<Orden>.NotFound("La orden referenciada no fue encontrada.");
            if (orden.pagada) Result<Orden>.BadRequest(default, "La orden referenciada ya fue pagada.");
            if (orden.cancelada) Result<Orden>.BadRequest(default, "La orden referenciada se encuentra anulada.");
            var ordenEntrada = _repoEntrada.GetEntradasXOrden(idOrden);
            if (ordenEntrada.Count() > 0) Result<Orden>.BadRequest(default, "La orden referenciada ya posee una entrada.");
            var tarifa = _repoTarifa.DetalleTarifa(idTarifa);
            if (tarifa.stock <= 0) return Result<Orden>.BadRequest(default, "La tarifa seleccionada ya no dispone de stock.");
            Entrada entrada = new Entrada
            {
                idOrden = idOrden,
                idTarifa = idTarifa
            };
            _repoEntrada.AltaEntrada(entrada);
            _repoOrden.AgregarPrecio(orden.idOrden, tarifa.precio);
            _repoEntrada.RestarStock(tarifa.idTarifa);
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
            idCliente = ordenDto.idCliente,
            fecha = DateTime.Now
        };
    }
}