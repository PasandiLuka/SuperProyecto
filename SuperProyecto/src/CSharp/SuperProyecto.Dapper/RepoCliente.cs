using System.Data;
using Dapper;

using SuperProyecto.Core.Services.Persistencia;
using SuperProyecto.Core;

namespace SuperProyecto.Dapper;

public class RepoCliente : Repo, IRepoCliente
{
    public RepoCliente(IDbConnection conexion) : base(conexion) { }

    private static readonly string _queryClientes
        = "SELECT * FROM Cliente";
    public IEnumerable<Cliente> GetClientes() => _conexion.Query<Cliente>(_queryClientes);


    private static readonly string _queryAltaCliente
        = @"INSERT INTO Cliente (DNI, nombre, apellido, telefono) VALUES (@DNI, @nombre, @apellido, @telefono)";
    public void AltaCliente(Cliente cliente)
    {
        _conexion.Execute(_queryAltaCliente, new { cliente.DNI, cliente.nombre, cliente.apellido, cliente.telefono });
    }


    private static readonly string _queryDetalleCliente
        = @"SELECT * FROM Cliente WHERE DNI = @DNI";
    public Cliente? DetalleCliente(int DNI)
    {
        return _conexion.QueryFirstOrDefault<Cliente>(_queryDetalleCliente, new { DNI });
    }

    private static readonly string _queryUpdateCliente
        = @"UPDATE Cliente SET DNI = @unDNI, nombre = @unNombre, apellido = @unApellido, telefono = @unTelefono WHERE DNI = @unId";
    public void UpdateCliente(Cliente cliente, int id)
    {
        _conexion.Execute(_queryUpdateCliente, new { unId = id, unDNI = cliente.DNI, unNombre = cliente.nombre, unApellido = cliente.apellido, unTelefono = cliente.telefono });
    }
}