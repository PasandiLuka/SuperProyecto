using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.IServices;
using MySqlConnector;

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

    public Result<IEnumerable<Entrada>> GetEntradas()
    {
        try
        {
            return Result<IEnumerable<Entrada>>.Ok(_repoEntrada.GetEntradas());
        }
        catch (MySqlException)
        {
            return Result<IEnumerable<Entrada>>.Unauthorized();
        }
    }

    public  Result<Entrada?> DetalleEntrada(int id) => Result<Entrada?>.Ok(_repoEntrada.DetalleEntrada(id));

    public Result<byte[]?> GetQr(int id)
    {
        try
        {
            var qr = _repoQr.DetalleQr(id);
            if (qr is null) return Result<byte[]?>.BadRequest(default, "QR no encontrado.");
            return Result<byte[]?>.File(_qrService.CrearQR(qr.url));
        }
        catch (MySqlException)
        {
            return Result<byte[]?>.Unauthorized();
        }
    }

    public Result<object> ValidarQr(int id)
    {
        try
        {
            var entrada = _repoEntrada.DetalleEntrada(id);
            if (entrada is null) return Result<object>.BadRequest(default, "Entrada no valida.");
            if (entrada.usada) return Result<object>.BadRequest(default, "La entrada ya fue usada.");
            _repoEntrada.EntradaUsada(entrada.idEntrada);
            return Result<object>.Ok();     
        }
        catch (MySqlException)
        {
            return Result<object>.Unauthorized();
        }
    }
}