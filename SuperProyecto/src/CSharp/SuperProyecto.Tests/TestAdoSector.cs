using SuperProyecto.Core;
using SuperProyecto.Core.Persistencia;
using SuperProyecto.Dapper;
using MySqlConnector;
using Moq;

namespace SuperProyecto.Tests;

public class TestAdoSector 
{
    private IRepoSector _repoSector;

   
    [Fact]
    public void CuandoHaceUnInsertEnTarifa_DebeAlmacenarDichaFilaEnLaTablaTarifa1()
        {
        var moq = new Mock<IRepoSector>();

        Sector sector  = new Sector {  idSector = 1,idLocal =1, nombre="Casimiro" };

        moq.Setup(t => t.AltaSector(sector));
        moq.Setup(t => t.DetalleSector(sector.idSector)).Returns(sector);
        var resultado = moq.Object.DetalleSector(sector.idSector);

        Assert.NotNull(resultado);
        Assert.Equal(sector.idSector, resultado.idSector);
        }

    [Fact]
    public void Cuando_se_agrega_una_nueva_Sector_se_crea_nuevos_valores_de_las_variables()
    {
        // Arrange
        var moq = new Mock<IRepoSector>();
        int idSector = 2;
        int idLocal =2;
        string nombre = "Sector Vip";
        var sector = new Sector { idSector = idSector, idLocal = idLocal, nombre = nombre };

        // Act
        moq.Object.AltaSector(sector);

        // Assert
        moq.Verify(r => r.AltaSector(It.Is<Sector>(t =>
            t.idSector == idSector &&
            t.idLocal == idLocal &&
            t.nombre == nombre
        )), Times.Once);
    }
}
 