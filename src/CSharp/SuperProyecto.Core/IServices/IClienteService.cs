using SuperProyecto.Core.Entidades;

namespace SuperProyecto.Core.IServices;

public interface IClienteService
{
    IEnumerable<Cliente> GetClientes();
    Cliente? DetalleCliente(int id);
    void AltaCliente(Cliente cliente);
    void UpdateCliente(Cliente cliente, int id);
}