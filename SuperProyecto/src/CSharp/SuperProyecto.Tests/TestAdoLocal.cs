

using SuperProyecto.Core;
using SuperProyecto.Core.Services.Persistencia;
using SuperProyecto.Dapper;
using MySqlConnector;

namespace SuperProyecto.Tests;

public class TestAdoLocal : TestAdo
{
    private IRepoLocal _repoLocal;

    public TestAdoLocal()
    {
        _repoLocal = new RepoLocal(_conexion);
    }

    [Fact]
    public void CuandoHaceUnInsertEnLocal_DebeAlmacenarDichaFilaEnLaTablaLocal()
    {
        var _local = new Local()
        {
            idLocal = 300,
            direccion = "vale_por_una_direccion",
            capacidadMax = 600
        };

        _repoLocal.AltaLocal(_local);

        var _localDB = _repoLocal.DetalleLocal(300);

        Assert.NotNull(_localDB);
        Assert.Equal(_local.idLocal, _localDB.idLocal);
        Assert.Equal(_local.direccion, _localDB.direccion);
        Assert.Equal(_local.capacidadMax, _localDB.capacidadMax);
    }

    [Fact]
    public void CuandoHagoUnInsertConUnaPKDuplicada_DebeTirarUnaExcepcion()
    {
        var _local = new Local()
        {
            idLocal = 301,
            direccion = "vale_por_una_direccion",
            capacidadMax = 700
        };

        _repoLocal.AltaLocal(_local);

        Assert.Throws<MySqlException>(() => _repoLocal.AltaLocal(_local));
    }

    [Fact]
    public void CuandoHagoUnUpdateEnLaTablaLocal_DebeHacerLasRespectivasModificaciones()
    {
        var _local = new Local()
        {
            idLocal = 300,
            direccion = "vale_por_una_direccion",
            capacidadMax = 600
        };

        _repoLocal.AltaLocal(_local);

        var _localUpdate = new Local()
        {
            idLocal = 1,
            direccion = "vale_por_una_direccionUpdate",
            capacidadMax = 200
        };

        _repoLocal.UpdateLocal(_localUpdate, _local.idLocal);

        var _localDB = _repoLocal.DetalleLocal(_local.idLocal);

        Assert.NotNull(_localDB);
        Assert.Equal(_local.idLocal, _localDB.idLocal);
        Assert.Equal(_localUpdate.direccion, _localDB.direccion);
        Assert.Equal(_localUpdate.capacidadMax, _localDB.capacidadMax);
    }
}