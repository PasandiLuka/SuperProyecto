using System.Data;
using Dapper;

using SuperProyecto.Core.Services.Persistencia;
using SuperProyecto.Core;
using System.Linq.Expressions;

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
        = @"INSERT INTO Cliente (DNI, nombre, apellido, email, telefono) VALUES (@unDNI, @unNombre, @unApellido, @unEmail, @unTelefono)";
    public void AltaCliente(Cliente cliente)
    {
        _conexion.Execute(_queryAltaCliente, new { unDNI = cliente.DNI, unNombre = cliente.nombre, unApellido = cliente.apellido, unEmail = cliente.email, unTelefono = cliente.telefono });
    }

    private static readonly string _queryUpdateCliente
        = @"UPDATE Cliente SET nombre = @nombre, apellido = @apellido, email = @email, telefono = @telefono WHERE DNI = @DNI";
    public void UpdateCliente(Cliente cliente, int id)
    {
        _conexion.Execute(
            _queryUpdateCliente,
            new
            {
                cliente.nombre,
                cliente.apellido,
                cliente.email,
                cliente.telefono,
                DNI = id
            });
    }
}