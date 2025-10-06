using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SuperProyecto.Core.DTO;
using Microsoft.AspNetCore.Authorization;

namespace SuperProyecto.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdenController : ControllerBase
{
    readonly IRepoOrden _repoOrden;
    readonly IRepoEntrada _repoEntrada;

    public OrdenController(IRepoOrden repoOrden, IRepoEntrada repoEntrada)
    {
        _repoOrden = repoOrden;
        _repoEntrada = repoEntrada;
    }

    [Authorize]
    [HttpGet]
    public IActionResult GetOrdenes()
    {
        var ordenes = _repoOrden.GetOrdenes();
        return ordenes.Any() ? Ok(ordenes) : NoContent();
    }

    [Authorize]
    [HttpGet("{id}")]
    public IActionResult DetalleOrden(int id)
    {
        var orden = _repoOrden.DetalleOrden(id);
        return orden is not null ? Ok(orden) : NotFound();
    }

    [Authorize(Roles = "Administrador, Organizador")]
    [HttpPost]
    public IActionResult AltaOrden([FromBody] OrdenDto ordenDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var ordenAlta = new Orden
        {
            DNI = ordenDto.DNI,
            idFuncion = ordenDto.idFuncion,
            fecha = DateTime.Now,
        };
        _repoOrden.AltaOrden(ordenAlta);
        return Created();
    }

    /* [Authorize]
    [HttpPost]
    public IActionResult AltaEntrada([FromBody] EntradaDto entradaDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        _repoEntrada.AltaEntrada(entradaAlta);

        string qrUrl = Url.Action(
        action: "ValidarQr",
        controller: "Entrada",
        values: new { idEntrada = entradaAlta.idEntrada },
        protocol: Request.Scheme // Usa "http" o "https" según el request actual
        );

        _repoQr.AltaQr(entradaDto.idEntrada, qrUrl);
        return Created();
    } */


    [Authorize(Roles = "Administrador, Organizador")]
    [HttpPost("{id}/pagar")]
    public IActionResult PagarOrden(int id)
    {
        var orden = _repoOrden.DetalleOrden(id);
        if (orden is null) return NotFound();
        _repoOrden.PagarOrden(id);
        var entradaAlta = new Entrada
        {           
            idOrden = id,
            usada = false
        };
        _repoEntrada.AltaEntrada(entradaAlta);
        return Ok();
    }
}