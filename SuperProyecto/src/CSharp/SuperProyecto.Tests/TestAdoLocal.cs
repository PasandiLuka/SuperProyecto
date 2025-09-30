

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
        var local = new Local()
        {
            idLocal = 300,
            direccion = "vale_por_una_direccion",
            capacidadMax = 600
        };

        _repoLocal.AltaLocal(local);

        var localDB = _repoLocal.DetalleLocal(300);

        Assert.NotNull(localDB);
        Assert.Equal(local.idLocal, localDB.idLocal);
        Assert.Equal(local.direccion, localDB.direccion);
        Assert.Equal(local.capacidadMax, localDB.capacidadMax);
    }

    [Fact]
    public void CuandoHagoUnInsertConUnaPKDuplicada_DebeTirarUnaExcepcion()
    {
        var local = new Local()
        {
            idLocal = 301,
            direccion = "vale_por_una_direccion",
            capacidadMax = 700
        };

        _repoLocal.AltaLocal(local);

        Assert.Throws<MySqlException>(() => _repoLocal.AltaLocal(local));
    }

    
}