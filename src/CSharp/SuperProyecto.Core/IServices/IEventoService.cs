using SuperProyecto.Core.Entidades;

namespace SuperProyecto.Core.IServices;

public interface IEventoService
{
    public IEnumerable<Evento> GetEventos();

    public Evento? DetalleEvento(int id);

    public void UpdateEvento(Evento evento, int id);

    public void AltaEvento(Evento evento);

    public void CancelarEvento(int id);

    public void PublicarEvento(int id);
}