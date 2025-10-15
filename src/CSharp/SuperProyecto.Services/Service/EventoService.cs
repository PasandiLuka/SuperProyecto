using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.IServices;
using SuperProyecto.Core.DTO;

namespace SuperProyecto.Services.Service;

public class EventoService : IEventoService
{
    readonly IRepoEvento _repoEvento;

    public EventoService(IRepoEvento repoEvento)
    {
        _repoEvento = repoEvento;
    }

    public IEnumerable<Evento> GetEventos() => _repoEvento.GetEventos();

    public Evento? DetalleEvento(int id) => _repoEvento.DetalleEvento(id);

    public void UpdateEvento(EventoDto eventoDto, int id)
    {
        Evento evento = ConvertirDtoClase(eventoDto);
        _repoEvento.UpdateEvento(evento, id);
    }

    public void AltaEvento(EventoDto eventoDto)
    {
        Evento evento = ConvertirDtoClase(eventoDto);
        _repoEvento.AltaEvento(evento);
    }

    public void CancelarEvento(int id) => _repoEvento.CancelarEvento(id);

    public void PublicarEvento(int id) => _repoEvento.PublicarEvento(id);

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