using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SuperProyecto.Core;
using Microsoft.AspNetCore.Authorization;
using SuperProyecto.Core.DTO;

namespace SuperProyecto.Api.Controllers;

[Authorize]
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

    [HttpGet]
    public IActionResult GetEntradas()
    {
        var entradas = _repoEntrada.GetEntradas();
        return entradas.Any() ? Ok(entradas) : NoContent();
    }

    [HttpGet("{id}")]
    public IActionResult DetalleEntrada(int id)
    {
        var entrada = _repoEntrada.DetalleEntrada((int)id);
        return entrada is not null ? Ok(entrada) : NotFound();
    }

    [HttpPost]
    public IActionResult AltaEntrada([FromBody] EntradaDto entradaDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var entradaAlta = new Entrada
        {
            idEntrada = entradaDto.idEntrada,
            idFuncion = entradaDto.idFuncion,
            idOrden = entradaDto.idOrden,
            idQr = entradaDto.idEntrada,
            usada = false
        };
        _repoEntrada.AltaEntrada(entradaAlta);

        string qrUrl = Url.Action(
        action: "ValidarQr",
        controller: "Entrada",
        values: new { idEntrada = entradaAlta.idEntrada },
        protocol: Request.Scheme // Usa "http" o "https" según el request actual
        );

        _repoQr.AltaQr(entradaDto.idEntrada, qrUrl);
        return Created();
    }

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

        entrada.usada = true;
        _repoEntrada.EntradaUsada(entrada.idEntrada);

        return Ok("Entrada validada correctamente.");
    }
}