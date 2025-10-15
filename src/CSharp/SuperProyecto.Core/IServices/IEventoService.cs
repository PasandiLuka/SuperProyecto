using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.DTO;

namespace SuperProyecto.Core.IServices;

public interface IEventoService
{
    public IEnumerable<Evento> GetEventos();

    public Evento? DetalleEvento(int id);

    public void UpdateEvento(EventoDto evento, int id);

    public void AltaEvento(EventoDto evento);

    public void CancelarEvento(int id);

    public void PublicarEvento(int id);
}