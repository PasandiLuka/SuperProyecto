using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.IServices;

namespace SuperProyecto.Services.Service;

public class EntradaService : IEntradaService
{
    readonly IRepoEntrada _repoEntrada;
    readonly IQrService _qrService;
    readonly IRepoQr _repoQr;
    public EntradaService(IRepoEntrada repoEntrada, IRepoQr repoQr, IQrService qrService)
    {
        _repoEntrada = repoEntrada;
        _qrService = qrService;
        _repoQr = repoQr;
    }

    public Result<IEnumerable<Entrada>> GetEntradas() => Result<IEnumerable<Entrada>>.Ok(_repoEntrada.GetEntradas());

    public  Result<Entrada?> DetalleEntrada(int id) => Result<Entrada?>.Ok(_repoEntrada.DetalleEntrada(id));

    public Result<byte[]?> GetQr(int id)
    {
        var qr = _repoQr.DetalleQr(id);
        if (qr is null) return Result<byte[]?>.BadRequest(default, "QR no encontrado.");
        return Result<byte[]?>.Ok(_qrService.CrearQR(qr.url));
    }

    public Result<object> ValidarQr(int id)
    {
        var entrada = _repoEntrada.DetalleEntrada(id);
        if (entrada is null) return Result<object>.BadRequest(default, "Entrada no encontrada.");
        if (entrada.usada) return Result<object>.BadRequest(default, "La entrada ya fue usada.");
        _repoEntrada.EntradaUsada(entrada.idEntrada);
        return Result<object>.Ok();
    }
}