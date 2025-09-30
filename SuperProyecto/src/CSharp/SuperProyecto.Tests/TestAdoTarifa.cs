using SuperProyecto.Core;
using SuperProyecto.Core.Services.Persistencia;
using SuperProyecto.Dapper;
using MySqlConnector;

namespace SuperProyecto.Tests;

public class TestAdoTarifa : TestAdo
{
    private IRepoTarifa _repoTarifa;

    public TestAdoTarifa()
    {
        _repoTarifa = new RepoTarifa(_conexion);
    }


    [Fact]
    public void CuandoHaceUnInsertEnTarifa_DebeAlmacenarDichaFilaEnLaTablaTarifa()
    {
        var _tarifa = new Tarifa()
        {
            idTarifa = 200,
            precio =20000
        };

        _repoTarifa.AltaTarifa(_tarifa);

        var tarifaBD = _repoTarifa.DetalleTarifa(200);

        Assert.NotNull(tarifaBD);
        Assert.Equal(_tarifa.idTarifa, tarifaBD.idTarifa);
        Assert.Equal(_tarifa.precio, tarifaBD.precio);
    }

    [Fact]
    public void CuandoHagoUnInsertConUnaPKDuplicada_DebeTirarUnaExcepcion()
    {
        var _tarifa = new Tarifa()
        {
            idTarifa = 201,
            precio =20000
        };

        _repoTarifa.AltaTarifa(_tarifa);

        Assert.Throws<MySqlException>(() => _repoTarifa.AltaTarifa(_tarifa));
    }
}