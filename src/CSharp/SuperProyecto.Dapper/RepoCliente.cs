using Dapper;

using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.DTO;

namespace SuperProyecto.Dapper;

public class RepoCliente : Repo, IRepoCliente
{
    public RepoCliente(IAdo _ado) : base(_ado) { }

    private static readonly string _queryClientes
        = "SELECT * FROM Cliente";
    public IEnumerable<ClienteResponse> GetClientes() => _conexion.Query<ClienteResponse>(_queryClientes); 
    
    private static readonly string _queryDetalleCliente 
        = @"SELECT * FROM Cliente WHERE idCliente = @idCliente"; 
    public Cliente? DetalleCliente(int idCliente) 
    {
        return _conexion.QueryFirstOrDefault<Cliente>(_queryDetalleCliente, new { idCliente });
    }

    private static readonly string _queryAltaCliente
        = @"INSERT INTO Cliente (idUsuario, DNI, nombre, apellido) VALUES (@unIdUsuario, @unDNI, @unNombre, @unApellido, @unTelefono)";
    public void AltaCliente(Cliente cliente)
    {
        _conexion.Execute(
            _queryAltaCliente,
            new
            {
                unIdUsuario = cliente.idUsuario,
                unDNI = cliente.DNI,
                unNombre = cliente.nombre,
                unApellido = cliente.apellido
            });
    }

    private static readonly string _queryUpdateCliente
        = @"UPDATE Cliente SET nombre = @nombre, apellido = @apellido WHERE idCliente = @idCliente";
    public void UpdateCliente(Cliente cliente, int idCliente)
    {
        _conexion.Execute(
            _queryUpdateCliente,
            new
            {
                cliente.nombre,
                cliente.apellido,
                idCliente
            });
    }
}