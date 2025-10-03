using SuperProyecto.Core;
using SuperProyecto.Core.Services.Persistencia;
using SuperProyecto.Dapper;
using MySqlConnector;

namespace SuperProyecto.Tests;

public class TestAdoSector : TestAdo
{
    private IRepoSector _repoSector;

    public TestAdoSector()
    {
        _repoSector = new RepoSector(_conexion);
    }

    [Fact]
    public void CuandoHaceUnInsertEnSector_DebeAlmacenarDichaFilaEnLaTablaSector()
    {
        var _sector = new Sector()
        {
            idSector = 200,
            sector = "unoa"
        };

        _repoSector.AltaSector(_sector);

        var sectorBD = _repoSector.DetalleSector(200);

        Assert.NotNull(sectorBD);
        Assert.Equal(_sector.idSector, sectorBD.idSector);
        Assert.Equal(_sector.sector, sectorBD.sector);
    }

    [Fact]
    public void CuandoHagoUnInsertConUnaPKDuplicada_DebeTirarUnaExcepci0n()
    {
        var _sector = new Sector()
        {
            idSector = 201,
            sector = "unoa"
        };

        _repoSector.AltaSector(_sector);

        Assert.Throws<MySqlException>(() =>_repoSector.AltaSector(_sector));
    }
    
    [Fact]
    public void CuandoHagoUnDelete_DeberiaEliminarElSector_Sinotiene_funciones_vigentes()
    {
         // Arrange
    var sectorId = 202;
    var sector = new Sector() { idSector = sectorId, sector = "Sector Test" };
    _repoSector.AltaSector(sector);

    // Act
    _repoSector.DeleteSector(sectorId);

    // Assert
    var sectorBD = _repoSector.DetalleSector(sectorId);
    Assert.Null(sectorBD);
    }
}
