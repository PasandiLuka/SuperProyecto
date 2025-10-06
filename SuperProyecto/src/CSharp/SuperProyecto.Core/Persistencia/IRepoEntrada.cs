namespace SuperProyecto.Core.Persistencia;

public interface IRepoEntrada
{
    IEnumerable<Entrada> GetEntradas();
    Entrada? DetalleEntrada(int idEntrada);
    void AltaEntrada(Entrada entrada);
    void EntradaUsada(int id);
}
