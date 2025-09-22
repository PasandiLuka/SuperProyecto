namespace SuperProyecto.Core.Persistencia;

public interface IRepoCliente
{
    IEnumerable<Cliente> GetClientes();
    void AltaCliente(Cliente cliente);
    Cliente? DetalleCliente(int DNI);
}