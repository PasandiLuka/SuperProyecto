namespace SuperProyecto.Core.Persistencia;

public interface IRepoEntrada
{
    IEnumerable<Entrada> GetEntradas();
    Entrada? DetalleEntrada(int idEntrada);
    void AltaEntrada(Entrada entrada);
    void UpdateEntrada(Entrada entrada, int id);
    void EntradaUsada(int id);
}
