using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.IServices;
using SuperProyecto.Core.DTO;
using System.Reflection.Metadata.Ecma335;

namespace SuperProyecto.Services.Service;

public class EventoService : IEventoService
{
    readonly IRepoEvento _repoEvento;

    public EventoService(IRepoEvento repoEvento)
    {
        _repoEvento = repoEvento;
    }

    public Result<IEnumerable<Evento>> GetEventos() => Result<IEnumerable<Evento>>.Ok(_repoEvento.GetEventos());

    public Result<Evento?> DetalleEvento(int id)
    {
        var evento = _repoEvento.DetalleEvento(id);
        if (evento is null) return Result<Evento?>.NotFound("El evento solicitado no fue encontrado.");
        return Result<Evento>.Ok(evento);
    }

    public Result<EventoDto> UpdateEvento(EventoDto eventoDto, int id)
    {
        if(_repoEvento.DetalleEvento(id) is null) return Result<EventoDto>.NotFound("El evento a modificar no fue encontrado.");
        Evento evento = ConvertirDtoClase(eventoDto);
        _repoEvento.UpdateEvento(evento, id);
        return Result<EventoDto>.Ok(eventoDto);
    }

    public Result<EventoDto> AltaEvento(EventoDto eventoDto)
    {
        Evento evento = ConvertirDtoClase(eventoDto);
        _repoEvento.AltaEvento(evento);
        return Result<EventoDto>.Ok(eventoDto);
    }

    public Result<Evento> CancelarEvento(int id)
    {
        _repoEvento.CancelarEvento(id);
        return Result<Evento>.Ok();
    }

    public Result<Evento> PublicarEvento(int id)
    {
        _repoEvento.PublicarEvento(id);
        return Result<Evento>.Ok();
    } 

    static Evento ConvertirDtoClase(EventoDto clienteDto)
    {
        return new Evento
        {
            nombre = clienteDto.nombre,
            descripcion = clienteDto.descripcion,
            fechaPublicacion = DateTime.Now,
            publicado = false,
            cancelado = false
        };
    }
}