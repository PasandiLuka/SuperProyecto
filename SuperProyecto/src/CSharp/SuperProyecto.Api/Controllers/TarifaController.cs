using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SuperProyecto.Core.DTO;
using Microsoft.AspNetCore.Authorization;

namespace SuperProyecto.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TarifaController : ControllerBase
    {
        readonly IRepoTarifa _repoTarifa;
        public TarifaController(IRepoTarifa repoTarifa)
        {
            _repoTarifa = repoTarifa;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetTarifas()
        {
            var tarifas = _repoTarifa.GetTarifa();
            return tarifas.Any() ? Ok(tarifas) : NoContent();
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult DetalleTarifa(int id)
        {
            var tarifa = _repoTarifa.DetalleTarifa(id);
            return tarifa is not null ? Ok(tarifa) : NotFound();
        }

        [Authorize(Roles = "Administrador, Organizador")]
        [HttpPut("{id}")]
        public IActionResult UpdateTarifa([FromBody] TarifaDto tarifaDto, int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var tarifa = _repoTarifa.DetalleTarifa(id);
            if (tarifa is null) return NotFound();
            var tarifaUpdate = new Tarifa
            {
                idTarifa = id,
                idFuncion = tarifaDto.idFuncion,
                nombre = tarifaDto.nombre,
                precio = tarifaDto.precio,
                stock = tarifaDto.stock
            };
            _repoTarifa.UpdateTarifa(tarifaUpdate, id);
            return Ok(tarifaUpdate);
        }

        [Authorize(Roles = "Administrador, Organizador")]
        [HttpPost]
        public IActionResult AltaTarifa([FromBody] TarifaDto tarifaDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var tarifaAlta = new Tarifa
            {
                idFuncion = tarifaDto.idFuncion,
                nombre = tarifaDto.nombre,
                precio = tarifaDto.precio,
                stock = tarifaDto.stock
            };
            _repoTarifa.AltaTarifa(tarifaAlta);
            return Created();
        }
    }
}