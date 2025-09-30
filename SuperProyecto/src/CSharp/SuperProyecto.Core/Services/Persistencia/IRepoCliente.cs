namespace SuperProyecto.Core.Services.Persistencia;

public interface IRepoCliente
{
    IEnumerable<Cliente> GetClientes();
    void AltaCliente(Cliente cliente);
    Cliente? DetalleCliente(int DNI);
    void UpdateCliente(Cliente cliente, int id);
}