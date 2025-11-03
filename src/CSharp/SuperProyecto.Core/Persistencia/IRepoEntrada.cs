using SuperProyecto.Core.Entidades;

namespace SuperProyecto.Core.Persistencia;

public interface IRepoEntrada
{
    IEnumerable<Entrada> GetEntradas();
    IEnumerable<Entrada> GetEntradasXOrden(int idOrden);
    Entrada? DetalleEntrada(int idEntrada);
    void AltaEntrada(Entrada entrada);
    void EntradaUsada(int id);
    void RestarStock(int id);
    void DevolverStock(int id);
}