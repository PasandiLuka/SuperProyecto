
using SuperProyecto.Core;
using SuperProyecto.Core.Persistencia;
using SuperProyecto.Dapper;
using MySqlConnector;

namespace SuperProyecto.Tests;

public class TestAdoFuncion : TestAdo
{
    private IRepoFuncion _repoFuncion;

    public TestAdoFuncion()
    {
        _repoFuncion = new RepoFuncion(_conexion);
    }

    [Fact]
    public void CuandoHaceUnInsertEnFuncion_DebeAlmacenarDichaFilaEnLaTablaFuncion()
    {
        var funcion = new Funcion()
        {
            idFuncion = 200,
            idEvento = 1,
            descripcion = "vale_por_una_descripcion",
            fechaHora = DateTime.Parse("2024-12-31 20:00:00")
        };

        _repoFuncion.AltaFuncion(funcion);

        var funcionDB = _repoFuncion.DetalleFuncion(200);

        Assert.NotNull(funcionDB);
        Assert.Equal(funcion.idFuncion, funcionDB.idFuncion);
        Assert.Equal(funcion.idEvento, funcionDB.idEvento);
        Assert.Equal(funcion.descripcion, funcionDB.descripcion);
        Assert.Equal(funcion.fechaHora, funcionDB.fechaHora);
    }

    [Fact]
    public void CuandoHagoUnInsertConUnaPKDuplicada_DebeTirarUnaExcepcion()
    {
        var funcion = new Funcion()
        {
            idFuncion = 201,
            idEvento = 1,
            descripcion = "vale_por_una_descripcion",
            fechaHora = DateTime.Parse("2024-12-31 20:00:00")
        };

        _repoFuncion.AltaFuncion(funcion);

        Assert.Throws<MySqlException>(() => _repoFuncion.AltaFuncion(funcion));
    }
}