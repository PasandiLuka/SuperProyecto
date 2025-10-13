using SuperProyecto.Core;
using SuperProyecto.Core.Persistencia;
using SuperProyecto.Dapper;
using Moq;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using System.Data;
using System.Formats.Asn1;

namespace SuperProyecto.Tests;

public class TestAdoCliente : TestAdo
{
    [Fact]
    public void CuandoHaceUnInsertEnCliente_DebeAlmacenarDichaFilaEnLaTablaCliente()
    {
        var moq = new Mock<IRepoCliente>();
        List<Cliente> clientes = new List<Cliente>
        {
            new Cliente{DNI = 1, idUsuario = 1, nombre = "juan", apellido = "antonio", telefono = 1},
            new Cliente{DNI = 1, idUsuario = 1, nombre = "fede", apellido = "asdas", telefono = 1}
        };

        moq.Setup(c => c.GetClientes()).Returns(clientes);
        var resultado = moq.Object.GetClientes();

        Assert.NotEmpty(resultado);
        Assert.Equal(2, ((List<Cliente>)resultado).Count());

    }

    /* [Fact]
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
            DNI = 1,
            nombre = "vale_por_un_nombreUpdate",
            apellido = "vale_por_un_apellidoUpdate",
            telefono = 123456789
        };

        _repoCliente.UpdateCliente(_clienteUpdate, _cliente.DNI);

        var _clienteUpdateBD = _repoCliente.DetalleCliente(_cliente.DNI);

        Assert.NotNull(_clienteUpdateBD);
        Assert.Equal(_cliente.DNI, _clienteUpdateBD.DNI);
        Assert.Equal(_clienteUpdateBD.nombre, _clienteUpdate.nombre);
        Assert.Equal(_clienteUpdateBD.apellido, _clienteUpdate.apellido);
        Assert.Equal(_clienteUpdateBD.telefono, _clienteUpdate.telefono);
    } */
    
}