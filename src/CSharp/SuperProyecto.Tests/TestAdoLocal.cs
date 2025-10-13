
using SuperProyecto.Core;
using SuperProyecto.Core.Persistencia;
using SuperProyecto.Dapper;
using Moq;
using MySqlConnector;

namespace SuperProyecto.Tests;

public class TestAdoLocal
{
    [Fact]
    public void CuandoHaceUnInsertEnLocal_DebeAlmacenarDichaFilaEnLaTablaLocal()
    {
        var moq = new Mock<IRepoLocal>();

        Local local = new Local { idLocal = 1, nombre = "Casimiro", direccion = "pipipi" };

        moq.Setup(t => t.AltaLocal(local));
        moq.Setup(t => t.DetalleLocal(local.idLocal)).Returns(local);
        var resultado = moq.Object.DetalleLocal(local.idLocal);

        Assert.NotNull(resultado);
        Assert.Equal(local.idLocal, resultado.idLocal);
    }
}
    // [Fact]
    // public void CuandoHagoUnInsertConUnaPKDuplicada_DebeTirarUnaExcepcion()
    // {
    //     var _local = new Local()
    //     {
    //         idLocal = 301,
    //         direccion = "vale_por_una_direccion",
    //         capacidadMax = 700
    //     };

    //     _repoLocal.AltaLocal(_local);

    //     Assert.Throws<MySqlException>(() => _repoLocal.AltaLocal(_local));
    // }

    // [Fact]
    // public void CuandoHagoUnUpdateEnLaTablaLocal_DebeHacerLasRespectivasModificaciones()
    // {
    //     var _local = new Local()
    //     {
    //         idLocal = 300,
    //         direccion = "vale_por_una_direccion",
    //         capacidadMax = 600
    //     };

    //     _repoLocal.AltaLocal(_local);

    //     var _localUpdate = new Local()
    //     {
    //         idLocal = 1,
    //         direccion = "vale_por_una_direccionUpdate",
    //         capacidadMax = 200
    //     };

    //     _repoLocal.UpdateLocal(_localUpdate, _local.idLocal);

    //     var _localDB = _repoLocal.DetalleLocal(_local.idLocal);

    //     Assert.NotNull(_localDB);
    //     Assert.Equal(_local.idLocal, _localDB.idLocal);
    //     Assert.Equal(_localUpdate.direccion, _localDB.direccion);
    //     Assert.Equal(_localUpdate.capacidadMax, _localDB.capacidadMax);
    // }

        // public void CuandoSolicitaSectoresPorLocal_DebeRetornarListaDeSectores()
        // {
        //     // Arrange
        //     var moq = new Mock<IRepoSector>();
        //     int localId = 1;
        //     var sectores = new List<Sector>
        //     {
        //         new Sector { idSector = 1, nombre = "Sector A", idLocal = localId },
        //         new Sector { idSector = 2, nombre = "Sector B", idLocal = localId }
        //     };

        //     moq.Setup(r => r.AltaTarifa(idTarifa)).Returns(sectores);

        //     // Act
        //     var resultado = moq.Object.GetSectoresPorLocal(localId);

        //     // Assert
        //     Assert.NotNull(resultado);
        //     Assert.Equal(2, resultado.Count);
        //     Assert.All(resultado, s => Assert.Equal(localId, s.idLocal));
        // }
