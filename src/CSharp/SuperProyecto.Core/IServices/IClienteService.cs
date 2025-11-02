using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.DTO;

namespace SuperProyecto.Core.IServices;

public interface IClienteService
{
    Result<IEnumerable<ClienteResponse>> GetClientes();
    Result<ClienteResponse> DetalleCliente(int id);
    Result<ClienteResponse> AltaCliente(ClienteDtoAlta cliente);
    Result<ClienteResponse> UpdateCliente(ClienteDtoUpdate cliente, int id);
}