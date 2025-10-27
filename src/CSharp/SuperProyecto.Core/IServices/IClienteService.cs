using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.DTO;

namespace SuperProyecto.Core.IServices;

public interface IClienteService
{
    Result<IEnumerable<Cliente>> GetClientes();
    Result<Cliente> DetalleCliente(int id);
    Result<Cliente> AltaCliente(ClienteDto cliente);
    Result<Cliente> UpdateCliente(ClienteDto cliente, int id);
}