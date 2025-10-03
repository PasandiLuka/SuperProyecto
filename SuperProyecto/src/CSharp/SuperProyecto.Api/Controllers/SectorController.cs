using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SuperProyecto.Core;

namespace SuperProyecto.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SectorController : ControllerBase
{
    readonly IReposector _repoSector;
    public SectorController(IRepoSector RepoSector )
    {
        _repoSector = RepoSector;
    }

    [HttpGet]
    public IActionResult GetSectores()
    {
        var sector = _repoSector.GetSectores();
        return sector.Any() ? Ok(sector) : NoContent();
    }

    [HttpGet("{id}")]
    public IActionResult DetalleSector(int id)
    {
        var sector = _repoSector.DetalleSector((int)id);
        return sector is not null ? Ok(sector) : NotFound();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateSector(int id, [FromBody] Tarifa sectorUpdate)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var sector = _repoSector.DetalleSector((int)id);
        if(sector is null)
            return NotFound();

        _repoTarifa.UpdateTarifa(sectorUpdate, (int)id);
        return Ok(sectorUpdate);
    }

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