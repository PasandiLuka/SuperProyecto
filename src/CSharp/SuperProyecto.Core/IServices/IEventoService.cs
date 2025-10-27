using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.DTO;

namespace SuperProyecto.Core.IServices;

public interface IEventoService
{
    Result<IEnumerable<Evento>> GetEventos();

    Result<Evento?> DetalleEvento(int id);

    Result<EventoDto> UpdateEvento(EventoDto evento, int id);

    Result<EventoDto> AltaEvento(EventoDto evento);

    Result<Evento> CancelarEvento(int id);

    Result<Evento> PublicarEvento(int id);
}