using Dapper;

using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.Entidades;

namespace SuperProyecto.Dapper;

public class RepoCliente : Repo, IRepoCliente
{
    public RepoCliente(IAdo _ado) : base(_ado) { }

    private static readonly string _queryClientes
        = "SELECT * FROM Cliente";
    public IEnumerable<Cliente> GetClientes() => _conexion.Query<Cliente>(_queryClientes); 

    
    private static readonly string _queryDetalleCliente 
        = @"SELECT * FROM Cliente WHERE DNI = @unDNI"; 
    public Cliente? DetalleCliente(int DNI) 
    {
        return _conexion.QueryFirstOrDefault<Cliente>(_queryDetalleCliente, new { unDNI = DNI });
    }


    private static readonly string _queryAltaCliente
        = @"INSERT INTO Cliente (DNI, idUsuario, nombre, apellido, telefono) VALUES (@unDNI, @unIdUsuario, @unNombre, @unApellido, @unTelefono)";
    public void AltaCliente(Cliente cliente)
    {
        _conexion.Execute(
            _queryAltaCliente,
            new
            {
                unDNI = cliente.DNI,
                unIdUsuario = cliente.idUsuario,
                unNombre = cliente.nombre,
                unApellido = cliente.apellido,
                unTelefono = cliente.telefono
            });
    }

    private static readonly string _queryUpdateCliente
        = @"UPDATE Cliente SET nombre = @nombre, apellido = @apellido, telefono = @telefono WHERE DNI = @DNI";
    public void UpdateCliente(Cliente cliente, int id)
    {
        _conexion.Execute(
            _queryUpdateCliente,
            new
            {
                cliente.nombre,
                cliente.idUsuario,
                cliente.apellido,
                cliente.telefono,
                DNI = id
            });
    }
}