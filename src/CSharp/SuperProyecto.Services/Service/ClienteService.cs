using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.IServices;

namespace SuperProyecto.Services.Service;

public class ClienteService : IClienteService
{
    readonly IRepoCliente _repoCliente;
    public ClienteService(IRepoCliente repoCliente)
    {
        _repoCliente = repoCliente;
    }

    public IEnumerable<Cliente> GetClientes() => _repoCliente.GetClientes();

    public Cliente? DetalleCliente(int id) => _repoCliente.DetalleCliente(id);

    public void AltaCliente(Cliente cliente) => _repoCliente.AltaCliente(cliente);

    public void UpdateCliente(Cliente cliente, int id) => _repoCliente.UpdateCliente(cliente, id);
}