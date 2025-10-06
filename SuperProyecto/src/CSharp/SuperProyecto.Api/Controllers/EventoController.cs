using SuperProyecto.Core.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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

        [Authorize]
        [HttpGet]
        public IActionResult GetEventos()
        {
            var eventos = _repoEvento.GetEventos();
            return eventos.Any() ? Ok(eventos) : NoContent();
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult DetalleEvento(int id)
        {
            var evento = _repoEvento.DetalleEvento(id);
            return evento is not null ? Ok(evento) : NotFound();
        }

        [Authorize(Roles = "Administrador, Organizador")]
        [HttpPut("{id}")]
        public IActionResult UpdateEvento([FromBody] EventoDto eventoDto, int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var evento = _repoEvento.DetalleEvento(id);
            if (evento is null) return NotFound();
            var eventoUpdate = new Evento
            {
                nombre = eventoDto.nombre,
                descripcion = eventoDto.descripcion,
                fechaPublicacion = DateTime.Now,
                publicado = eventoDto.publicado
            };
            _repoEvento.UpdateEvento(eventoUpdate, id);
            return Ok(eventoUpdate);
        }

        [Authorize(Roles = "Administrador, Organizador")]
        [HttpPost]
        public IActionResult AltaEvento([FromBody] Evento evento)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _repoEvento.AltaEvento(evento);
            return Created();
        }

        [Authorize(Roles = "Administrador, Organizador")]
        [HttpDelete("{idEvento}")]
        public IActionResult CancelarEvento(int idEvento)
        {
            if (_repoEvento.DetalleEvento(idEvento) is null) return NotFound();
            _repoEvento.CancelarEvento(idEvento);
            return Ok();
        }
        [Authorize(Roles = "Administrador, Organizador")]
        [HttpPost("{idEvento}/publicar")]
        public IActionResult PublicarEvento(int idEvento)
        {
            if (_repoEvento.DetalleEvento(idEvento) is null) return NotFound();
            _repoEvento.PublicarEvento(idEvento);
            return Ok();
        }
    }
}