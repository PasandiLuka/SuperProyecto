using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.IServices;
using MySqlConnector;

namespace SuperProyecto.Services.Service;

public class EntradaService : IEntradaService
{
    readonly IRepoEntrada _repoEntrada;
    readonly IQrService _qrService;
    readonly IRepoOrden _repoOrden;
    readonly IRepoTarifa _repoTarifa;
    readonly IRepoQr _repoQr;
    public EntradaService(IRepoTarifa repoTarifa, IRepoOrden repoOrden, IRepoEntrada repoEntrada, IRepoQr repoQr, IQrService qrService)
    {
        _repoEntrada = repoEntrada;
        _qrService = qrService;
        _repoQr = repoQr;
        _repoOrden = repoOrden;
        _repoTarifa = repoTarifa;
    }

    public Result<IEnumerable<Entrada>> GetEntradas() => Result<IEnumerable<Entrada>>.Ok(_repoEntrada.GetEntradas());

    public  Result<Entrada?> DetalleEntrada(int id) => Result<Entrada?>.Ok(_repoEntrada.DetalleEntrada(id));

    public Result<byte[]?> GetQr(int id)
    {
        var qr = _repoQr.DetalleQr(id);
        if (qr is null) return Result<byte[]?>.BadRequest(default, "QR no encontrado.");
        return Result<byte[]?>.File(_qrService.CrearQR(qr.url));
    }

    public Result<object> ValidarQr(int id)
    {
        var entrada = _repoEntrada.DetalleEntrada(id);
        if (entrada is null) return Result<object>.BadRequest(default, "Entrada no valida.");
        if (entrada.usada) return Result<object>.BadRequest(default, "La entrada ya fue usada.");
        if (entrada.anulada) return Result<object>.BadRequest(default, "La entrada se encuentra anulada.");
        _repoEntrada.EntradaUsada(entrada.idEntrada);
        return Result<object>.Ok();
    }

    public Result<Entrada> CancelarEntrada(int id)
    {
        var entrada = _repoEntrada.DetalleEntrada(id);
        if (entrada is null) return Result<Entrada>.NotFound("La entrada a cancelar no fue encontrada");
        if (entrada.anulada) return Result<Entrada>.BadRequest(default, "La entrada ya fue anulada");
        if (entrada.usada) return Result<Entrada>.BadRequest(default, "La entrada ya fue usada");
        _repoOrden.RestarPrecio(entrada.idOrden, _repoTarifa.DetalleTarifa(entrada.idTarifa).precio);
        _repoEntrada.DevolverStock(entrada.idTarifa);
        _repoEntrada.CancelarEntrada(id);
        return Result<Entrada>.Ok();
    }
}