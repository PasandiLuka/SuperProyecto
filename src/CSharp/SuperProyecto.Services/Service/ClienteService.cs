using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.Entidades;

namespace SuperProyecto.Services.Service;

public class ClienteService
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