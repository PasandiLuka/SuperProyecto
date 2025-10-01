namespace SuperProyecto.Core.Services.Persistencia;

public interface IRepoEvento
{
    IEnumerable<Evento> GetEventos();
    Evento? DetalleEvento(int idEvento);
    void AltaEvento(Evento evento);
    void UpdateEvento(Evento evento, int id);
}
