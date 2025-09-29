namespace SuperProyecto.Core.Persistencia;

public interface IRepoEntrada
{
    IEnumerable<Entrada> GetEntradas();
    void AltaEntrada(Entrada entrada);
    Entrada? DetalleEntrada(int idEntrada);
}
