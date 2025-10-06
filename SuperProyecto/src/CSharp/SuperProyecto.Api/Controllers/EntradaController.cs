using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SuperProyecto.Core;
using Microsoft.AspNetCore.Authorization;
using SuperProyecto.Core.DTO;

namespace SuperProyecto.Api.Controllers;


[Route("[controller]")]
public class EntradaController : ControllerBase
{
    readonly IRepoEntrada _repoEntrada;
    readonly QrService _qrService;
    readonly IRepoQr _repoQr;
    public EntradaController(IRepoEntrada repoEntrada, IRepoQr repoQr, QrService qrService)
    {
        _repoEntrada = repoEntrada;
        _qrService = qrService;
        _repoQr = repoQr;
    }

    [Authorize]
    [HttpGet]
    public IActionResult GetEntradas()
    {
        var entradas = _repoEntrada.GetEntradas();
        return entradas.Any() ? Ok(entradas) : NoContent();
    }

    [Authorize]
    [HttpGet("{id}")]
    public IActionResult DetalleEntrada(int id)
    {
        var entrada = _repoEntrada.DetalleEntrada((int)id);
        return entrada is not null ? Ok(entrada) : NotFound();
    }

    [Authorize]
    [HttpGet("{id}/qr")]
    public IActionResult GetQr(int id)
    {
        var qr = _repoQr.DetalleQr(id);
        if (qr is null) return NotFound();
        byte[] qrBytes = _qrService.CrearQR(qr.url);
        return File(qrBytes, "image/png");
    }

    [HttpGet("qr/validar")]
    public IActionResult ValidarQr([FromQuery] int idEntrada)
    {
        var entrada = _repoEntrada.DetalleEntrada(idEntrada);
        if (entrada is null) return NotFound("Entrada no encontrada.");

        if (entrada.usada)
            return BadRequest("La entrada ya fue usada.");

        _repoEntrada.EntradaUsada(entrada.idEntrada);

        return Ok("Entrada validada correctamente.");
    }
}