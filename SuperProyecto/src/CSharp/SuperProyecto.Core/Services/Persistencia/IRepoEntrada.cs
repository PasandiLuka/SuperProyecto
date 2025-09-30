namespace SuperProyecto.Core.Services.Persistencia;

public interface IRepoEntrada
{
    IEnumerable<Entrada> GetEntradas();
    void AltaEntrada(Entrada entrada);
    Entrada? DetalleEntrada(int idEntrada);
    void UpdateEntrada(Entrada entrada, int id);
}
