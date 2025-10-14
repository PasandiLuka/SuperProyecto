using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.IServices;

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

    public void UpdateEvento(Evento evento, int id) => _repoEvento.UpdateEvento(evento, id);

    public void AltaEvento(Evento evento) => _repoEvento.AltaEvento(evento);

    public void CancelarEvento(int id) => _repoEvento.CancelarEvento(id);

    public void PublicarEvento(int id) => _repoEvento.PublicarEvento(id);
}