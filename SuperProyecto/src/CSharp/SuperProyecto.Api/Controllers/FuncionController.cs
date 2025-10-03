
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SuperProyecto.Core;

namespace SuperProyecto.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class FuncionController : ControllerBase
{
    readonly IRepoFuncion _repoFuncion;
    public FuncionController(IRepoFuncion repoFuncion)
    {
        _repoFuncion = repoFuncion;
    }

    [HttpGet]
    public IActionResult GetFunciones()
    {
        var funciones = _repoFuncion.GetFunciones();
        return funciones.Any() ? Ok(funciones) : NoContent();
    }

    [HttpGet("{id}")]
    public IActionResult DetalleFuncion(int id)
    {
        var funciones = _repoFuncion.DetalleFuncion((int)id);
        return funciones is not null ? Ok(funciones) : NotFound();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateFuncion(int id, [FromBody] Funcion funcionUpdate)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var funcion = _repoFuncion.DetalleFuncion((int)id);
        if (funcion is null)
            return NotFound();

        _repoFuncion.UpdateFuncion(funcionUpdate, (int)id);
        return Ok(funcionUpdate);
    }
}
