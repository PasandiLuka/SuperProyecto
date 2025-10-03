using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SuperProyecto.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        readonly IRepoEvento _repoEvento;

        public EventoController(IRepoEvento repoEvento)
        {
            _repoEvento = repoEvento;
        }

        [HttpGet]
        public IActionResult GetEventos()
        {
            var eventos = _repoEvento.GetEventos();
            return eventos.Any() ? Ok(eventos) : NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult DetalleEvento(int id)
        {
            var evento = _repoEvento.DetalleEvento(id);
            return evento is not null ? Ok(evento) : NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEvento([FromBody] Evento eventoUpdate, int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var evento = _repoEvento.DetalleEvento(id);
            if (evento is null) return NotFound();
            _repoEvento.UpdateEvento(eventoUpdate, id);
            return Ok(eventoUpdate);
        }

        [HttpPost]
        public IActionResult AltaEvento([FromBody] Evento evento)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _repoEvento.AltaEvento(evento);
            return Created();
        }
    }
}