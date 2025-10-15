using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.DTO;

namespace SuperProyecto.Core.IServices;

public interface IClienteService
{
    IEnumerable<Cliente> GetClientes();
    Cliente? DetalleCliente(int id);
    void AltaCliente(ClienteDto cliente);
    void UpdateCliente(ClienteDto cliente, int id);
}