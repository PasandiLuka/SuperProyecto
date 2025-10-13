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
public class FuncionController : ControllerBase
{
    readonly IRepoFuncion _repoFuncion;

    public FuncionController(IRepoFuncion repoFuncion)
    {
        _repoFuncion = repoFuncion;
    }
    
    [Authorize]
    [HttpGet]
    public IActionResult GetFunciones()
    {
        var funciones = _repoFuncion.GetFunciones();
        return funciones.Any() ? Ok(funciones) : NoContent();
    }

    [Authorize]
    [HttpGet("{id}")]
    public IActionResult DetalleFuncion(int id)
    {
        var funciones = _repoFuncion.DetalleFuncion((int)id);
        return funciones is not null ? Ok(funciones) : NotFound();
    }

    [Authorize(Roles = "Administrador, Organizador")]
    [HttpPut("{id}")]
    public IActionResult UpdateFuncion(int id, [FromBody] FuncionDto funcionDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var funcion = _repoFuncion.DetalleFuncion((int)id);
        if (funcion is null) return NotFound();
        var funcionUpdate = new Funcion
        {
            idFuncion = id,
            idTarifa = funcionDto.idTarifa,
            fechaHora = funcionDto.fechaHora,
            stock = funcionDto.stock
        };
        _repoFuncion.UpdateFuncion(funcionUpdate, (int)id);
        return Ok(funcionUpdate);
    }

    [Authorize(Roles = "Administrador, Organizador")]
    [HttpPost]
    public IActionResult AltaFuncion([FromBody] FuncionDto funcionDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var funcionAlta = new Funcion
        {
            idEvento = funcionDto.idEvento,
            idTarifa = funcionDto.idTarifa,
            fechaHora = funcionDto.fechaHora,
            stock = funcionDto.stock
        };
        _repoFuncion.AltaFuncion(funcionAlta);
        return Created();
    }
}