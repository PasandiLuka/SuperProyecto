using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SuperProyecto.Core.DTO;

namespace SuperProyecto.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocalController : ControllerBase
{
    readonly IRepoLocal _repoLocal;
    public LocalController(IRepoLocal repoLocal)
    {
        _repoLocal = repoLocal;
    }

    [Authorize]
    [HttpGet]
    public IActionResult GetLocales()
    {
        var locales = _repoLocal.GetLocales();
        return locales.Any() ? Ok(locales) : NoContent();
    }

    [Authorize]
    [HttpGet("{id}")]
    public IActionResult DetalleLocal(int id)
    {
        var local = _repoLocal.DetalleLocal((int)id);
        return local is not null ? Ok(local) : NotFound();
    }

    [Authorize(Roles = "Administrador, Organizador")]
    [HttpPut("{id}")]
    public IActionResult UpdateLocal(int id, [FromBody] LocalDto localDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var local = _repoLocal.DetalleLocal((int)id);
        if(local is null) return NotFound();
        var localUpdate = new Local
        {
            nombre = localDto.nombre,
            direccion = localDto.direccion
        };
        _repoLocal.UpdateLocal(localUpdate, (int)id);
        return Ok(localUpdate);
    }

    [Authorize(Roles = "Administrador, Organizador")]
    [HttpPost]
    public IActionResult AltaLocal([FromBody] LocalDto localDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var localAlta = new Local
        {
            nombre = localDto.nombre,
            direccion = localDto.direccion
        };
        _repoLocal.AltaLocal(localAlta);
        return Created();
    }

    [Authorize(Roles = "Administrador, Organizador")]
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