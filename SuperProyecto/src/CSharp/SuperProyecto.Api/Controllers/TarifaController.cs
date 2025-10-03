using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SuperProyecto.Core;

namespace SuperProyecto.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TarifaController : ControllerBase
{
    readonly IRepotarifa _repoTarifa;
    public TarifaController(IRepotarifa repoTarifa )
    {
        _repoTarifa = repoTarifa;
    }

    [HttpGet]
    public IActionResult GetTarifa()
    {
        var tarifa = _repoTarifa.GetTarifa();
        return tarifa.Any() ? Ok(tarifa) : NoContent();
    }

    [HttpGet("{id}")]
    public IActionResult DetalleTarifa(int id)
    {
        var tarifa = _repoTarifa.DetalleTarifa((int)id);
        return tarifa is not null ? Ok(tarifa) : NotFound();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateTarifa(int id, [FromBody] Tarifa tarifaUpdate)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var tarifa = _repoTarifa.DetalleTarifa((int)id);
        if(tarifa is null)
            return NotFound();

        _repoTarifa.UpdateTarifa(tarifaUpdate, (int)id);
        return Ok(tarifaUpdate);
    }
}