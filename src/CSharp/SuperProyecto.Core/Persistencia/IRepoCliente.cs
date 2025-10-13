using SuperProyecto.Core.Entidades;

namespace SuperProyecto.Core.Persistencia;

public interface IRepoCliente
{
    IEnumerable<Cliente> GetClientes();
    Cliente? DetalleCliente(int idCliente);
    void AltaCliente(Cliente cliente);
    void UpdateCliente(Cliente cliente, int id);
}