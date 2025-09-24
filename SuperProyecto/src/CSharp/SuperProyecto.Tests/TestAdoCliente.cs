using System.Data;
using SuperProyecto.Core;
using SuperProyecto.Core.Persistencia;
using SuperProyecto.Dapper;
using MySqlConnector;

namespace SuperProyecto.Tests;

public class TestAdoCliente
{
    protected IDbConnection _conexion { get; private set; }
    private IRepoCliente _repoCliente;

    public TestAdoCliente()
    {
        string cadenaConexion = @"Server=localhost;Database=bd_boleteria;Uid=5to_agbd;Pwd=Trigg3rs!;";
        _conexion = new MySqlConnection(cadenaConexion);
        _repoCliente = new RepoCliente(_conexion);
    }


    [Fact]
    public void CuandoHaceUnInsertEnCliente_DebeAlmacenarDichaFilaEnLaTablaCliente()
    {
        var _cliente = new Cliente()
        {
            DNI = 200,
            nombre = "vale_por_un_nombre",
            apellido = "vale_por_un_apellido",
            telefono = 12345678
        };

        _repoCliente.AltaCliente(_cliente);

        var clienteDB = _repoCliente.DetalleCliente(200);

        Assert.NotNull(clienteDB);
        Assert.Equal(_cliente.DNI, clienteDB.DNI);
        Assert.Equal(_cliente.nombre, clienteDB.nombre);
        Assert.Equal(_cliente.apellido, clienteDB.apellido);
        Assert.Equal(_cliente.telefono, clienteDB.telefono);
    }

    [Fact]
    public void CuandoHagoUnInsertConUnaPKDuplicada_DebeTirarUnaExcepcion()
    {
        var _cliente = new Cliente()
        {
            DNI = 201,
            nombre = "vale_por_un_nombre",
            apellido = "vale_por_un_apellido",
            telefono = 12345678
        };

        _repoCliente.AltaCliente(_cliente);

        Assert.Throws<MySqlException>(() => _repoCliente.AltaCliente(_cliente));
    }
}