namespace SuperProyecto.Core.Services.Persistencia;

public interface IRepoCliente
{
    IEnumerable<Cliente> GetClientes();
    Cliente? DetalleCliente(int DNI);
    void AltaCliente(Cliente cliente);
    void UpdateCliente(Cliente cliente, int id);
}