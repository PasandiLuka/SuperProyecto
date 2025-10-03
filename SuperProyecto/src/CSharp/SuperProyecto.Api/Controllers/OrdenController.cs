using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SuperProyecto.Core.DTO;

namespace SuperProyecto.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdenController : ControllerBase
{
    readonly IRepoOrden _repoOrden;

    public OrdenController(IRepoOrden repoOrden)
    {
        _repoOrden = repoOrden;
    }

    [HttpGet]
    public IActionResult GetOrdenes()
    {
        var ordenes = _repoOrden.GetOrdenes();
        return ordenes.Any() ? Ok(ordenes) : NoContent();
    }

    [HttpGet("{id}")]
    public IActionResult DetalleOrden(int id)
    {
        var orden = _repoOrden.DetalleOrden(id);
        return orden is not null ? Ok(orden) : NotFound();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateOrden([FromBody] OrdenDto ordenDto, int id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var orden = _repoOrden.DetalleOrden(id);
        if (orden is null) return NotFound();
        var ordenUpdate = new Orden
        {
            idOrden = id,
            DNI = ordenDto.DNI,
            fecha = DateTime.Now,
            total = ordenDto.total
        };
        _repoOrden.UpdateOrden(ordenUpdate, id);
        return Ok(ordenUpdate);
    }

    [HttpPost]
    public IActionResult AltaOrden([FromBody] OrdenDto ordenDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var ordenAlta = new Orden
        {
            DNI = ordenDto.DNI,
            fecha = DateTime.Now,
            total = ordenDto.total
        };
        _repoOrden.AltaOrden(ordenAlta);
        return Created();
    }
}