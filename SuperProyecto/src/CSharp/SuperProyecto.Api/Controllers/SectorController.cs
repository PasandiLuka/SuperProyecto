using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SuperProyecto.Core.DTO;
using Microsoft.AspNetCore.Authorization;

namespace SuperProyecto.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SectorController : ControllerBase
{
    readonly IRepoSector _repoSector;
    public SectorController(IRepoSector RepoSector)
    {
        _repoSector = RepoSector;
    }

    [Authorize]
    [HttpGet]
    public IActionResult GetSectores()
    {
        var sector = _repoSector.GetSectores();
        return sector.Any() ? Ok(sector) : NoContent();
    }

    [Authorize]
    [HttpGet("{id}")]
    public IActionResult DetalleSector(int id)
    {
        var sector = _repoSector.DetalleSector((int)id);
        return sector is not null ? Ok(sector) : NotFound();
    }

    [Authorize(Roles = "Administrador, Organizador")]
    [HttpPut("{id}")]
    public IActionResult UpdateSector(int id, [FromBody] SectorDto sectorDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var sector = _repoSector.DetalleSector((int)id);
        if (sector is null) return NotFound();
        var sectorUpdate = new Sector
        {
            idLocal = sectorDto.idLocal,
            nombre = sectorDto.nombre,
            capacidad = sectorDto.capacidad
        };
        _repoSector.UpdateSector(sectorUpdate, (int)id);
        return Ok(sectorUpdate);
    }

    [Authorize(Roles = "Administrador, Organizador")]
    [HttpPost]
    public IActionResult AltaSector([FromBody] SectorDto sectorDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var sectorAlta = new Sector
        {
            idLocal = sectorDto.idLocal,
            nombre = sectorDto.nombre,
            capacidad = sectorDto.capacidad
        };
        _repoSector.AltaSector(sectorAlta);
        return Created();
    }

    [Authorize(Roles = "Administrador, Organizador")]
    [HttpDelete("{id}")]
    public IActionResult DeleteSector(int id)
    {
        var sector = _repoSector.DetalleSector(id);
        if (sector is null)
            return NotFound();
        try
        {
            _repoSector.DeleteSector(id);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            // Si tu repo lanza una excepción cuando hay funciones vigentes
            return Conflict(new { message = ex.Message });
        }
    }
}