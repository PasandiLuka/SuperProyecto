using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SuperProyecto.Core;

namespace SuperProyecto.Api.Controllers;

[Route("[controller]")]
public class EntradaController : ControllerBase
{
    readonly IRepoEntrada _repoEntrada;
    public EntradaController(IRepoEntrada repoEntrada)
    {
        _repoEntrada = repoEntrada;
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
    public IActionResult AltaEntrada([FromBody] Entrada entrada)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        _repoEntrada.AltaEntrada(entrada);

        return Created();
    }
}