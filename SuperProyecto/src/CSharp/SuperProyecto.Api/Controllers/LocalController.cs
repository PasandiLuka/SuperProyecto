using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SuperProyecto.Core;

namespace SuperProyecto.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class LocalController : ControllerBase
{
    readonly IRepoLocal _repoLocal;
    public LocalController(IRepoLocal repoLocal)
    {
        _repoLocal = repoLocal;
    }

    [HttpGet]
    public IActionResult GetLocales()
    {
        var locales = _repoLocal.GetLocales();
        return locales.Any() ? Ok(locales) : NoContent();
    }

    [HttpGet("{id}")]
    public IActionResult DetalleLocal(int id)
    {
        var local = _repoLocal.DetalleLocal((int)id);
        return local is not null ? Ok(local) : NotFound();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateLocal(int id, [FromBody] Local localUpdate)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var cliente = _repoLocal.DetalleLocal((int)id);
        if(cliente is null)
            return NotFound();

        _repoLocal.UpdateCliente(localUpdate, (int)id);
        return Ok(localUpdate);
    }
    [HttpDelete("{id}")]
     public IActionResult DeleteLocal(int id)
    {
        var local = _repoLocal.DetalleLocal(id);
         if (local is null)
         return NotFound();

         try
        {
             _repoLocal.DeleteLocal(id);
             return NoContent();
        }
             catch (InvalidOperationException ex)
       {
            // Si tu repo lanza una excepción cuando hay funciones vigentes
            return Conflict(new { message = ex.Message });
        }
   }
 

}
