using SuperProyecto.Core;
using SuperProyecto.Core.Services.Persistencia;
using SuperProyecto.Dapper;
using MySqlConnector;

namespace SuperProyecto.Tests;

public class TestAdoCliente : TestAdo
{
    private IRepoCliente _repoCliente;

    public TestAdoCliente()
    {
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

    [Fact]
    public void CuandoHagoUnUpdateEnLaTablaCliente_DebeHacerLasRespectivasModificaciones()
    {
        var _cliente = new Cliente()
        {
            DNI = 202,
            nombre = "vale_por_un_nombre",
            apellido = "vale_por_un_apellido",
            telefono = 12345678
        };

        _repoCliente.AltaCliente(_cliente);

        var _clienteUpdate = new Cliente()
        {
            DNI = 300,
            nombre = "vale_por_un_nombreUpdate",
            apellido = "vale_por_un_apellidoUpdate",
            telefono = 123456789
        };

        _repoCliente.UpdateCliente(_clienteUpdate, _cliente.DNI);

        var _clienteUpdateBD = _repoCliente.DetalleCliente(_clienteUpdate.DNI);

        Assert.NotNull(_clienteUpdateBD);
        Assert.Equal(_clienteUpdateBD.DNI, _clienteUpdate.DNI);
        Assert.Equal(_clienteUpdateBD.nombre, _clienteUpdate.nombre);
        Assert.Equal(_clienteUpdateBD.apellido, _clienteUpdate.apellido);
        Assert.Equal(_clienteUpdateBD.telefono, _clienteUpdate.telefono);
    }
}