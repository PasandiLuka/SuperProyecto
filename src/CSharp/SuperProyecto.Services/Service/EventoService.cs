using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.IServices;
using SuperProyecto.Core.DTO;
using SuperProyecto.Services.Validators;
using MySqlConnector;
namespace SuperProyecto.Services.Service;

public class EventoService : IEventoService
{
    readonly IRepoEvento _repoEvento;
    readonly EventoValidator _validador;

    public EventoService(IRepoEvento repoEvento, EventoValidator eventoValidator)
    {
        _repoEvento = repoEvento;
        _validador = eventoValidator;
    }

    public Result<IEnumerable<Evento>> GetEventos()
    {
        try
        {
            return Result<IEnumerable<Evento>>.Ok(_repoEvento.GetEventos());
        }
        catch (MySqlException)
        {
            return Result<IEnumerable<Evento>>.Unauthorized();
        }
    }

    public Result<Evento?> DetalleEvento(int id)
    {
        try
        {
            var evento = _repoEvento.DetalleEvento(id);
            if (evento is null) return Result<Evento?>.NotFound("El evento solicitado no fue encontrado.");
            return Result<Evento>.Ok(evento);
        }
        catch (MySqlException)
        {
            return Result<Evento?>.Unauthorized();
        }
    }

    public Result<EventoDto> UpdateEvento(EventoDto eventoDto, int id)
    {
        try
        {
        var resultado = _validador.Validate(eventoDto);
        if (!resultado.IsValid)
        {
            var listaErrores = resultado.Errors
                .GroupBy(a => a.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );
            return Result<EventoDto>.BadRequest(listaErrores);
        }
        if(_repoEvento.DetalleEvento(id) is null) return Result<EventoDto>.NotFound("El evento a modificar no fue encontrado.");
        Evento evento = ConvertirDtoClase(eventoDto);
        _repoEvento.UpdateEvento(evento, id);
        return Result<EventoDto>.Ok(eventoDto);     
        }
        catch (MySqlException)
        {
            return Result<EventoDto>.Unauthorized();
        }
    }

    public Result<EventoDto> AltaEvento(EventoDto eventoDto)
    {
        try
        {
            var resultado = _validador.Validate(eventoDto);
            if (!resultado.IsValid)
            {
                var listaErrores = resultado.Errors
                    .GroupBy(a => a.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    );
                return Result<EventoDto>.BadRequest(listaErrores);
            }
            Evento evento = ConvertirDtoClase(eventoDto);
            _repoEvento.AltaEvento(evento);
            return Result<EventoDto>.Ok(eventoDto);
        }
        catch (MySqlException)
        {
            return Result<EventoDto>.Unauthorized();
        }
    }

    public Result<Evento> CancelarEvento(int id)
    {
        try
        {
            if(_repoEvento.DetalleEvento(id) is null) return Result<Evento>.NotFound("El evento a cancelar no fue encontrado.");
            _repoEvento.CancelarEvento(id);
            return Result<Evento>.Ok();
        }
        catch (MySqlException)
        {
            return Result<Evento>.Unauthorized();
        }
    }

    public Result<Evento> PublicarEvento(int id)
    {
        try
        {
            if(_repoEvento.DetalleEvento(id) is null) return Result<Evento>.NotFound("El evento a publicar no fue encontrado.");
            _repoEvento.PublicarEvento(id);
            return Result<Evento>.Ok(); 
        }
        catch (MySqlException)
        {
            return Result<Evento>.Unauthorized();
        }
    } 

    static Evento ConvertirDtoClase(EventoDto eventoDto)
    {
        return new Evento
        {
            nombre = eventoDto.nombre,
            descripcion = eventoDto.descripcion,
            fechaPublicacion = DateTime.Now,
            publicado = eventoDto.publicado,
            cancelado = false
        };
    }
}