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


}
 