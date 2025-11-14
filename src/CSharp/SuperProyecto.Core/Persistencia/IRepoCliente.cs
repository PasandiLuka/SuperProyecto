using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.DTO;

namespace SuperProyecto.Core.Persistencia;

public interface IRepoCliente
{
    IEnumerable<ClienteResponse> GetClientes();
    Cliente? DetalleCliente(int idCliente);
    void AltaCliente(Cliente cliente);
    void UpdateCliente(Cliente cliente, int id);
    Cliente? DetalleClienteXIdUsuario(int idUsuario);
}