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

    public IEnumerable<Entrada> GetEntradas() => _repoEntrada.GetEntradas();

    public Entrada? DetalleEntrada(int id) => _repoEntrada.DetalleEntrada(id);

    public byte[]? GetQr(int id)
    {
        var qr = _repoQr.DetalleQr(id);
        if (qr is null) return null;
        return _qrService.CrearQR(qr.url);
    }

    public string? ValidarQr(int id)
    {
        var entrada = _repoEntrada.DetalleEntrada(id);
        if (entrada is null) return "Entrada no encontrada.";
        if (entrada.usada) return "La entrada ya fue usada.";
        _repoEntrada.EntradaUsada(entrada.idEntrada);
        return null;
    }
}
