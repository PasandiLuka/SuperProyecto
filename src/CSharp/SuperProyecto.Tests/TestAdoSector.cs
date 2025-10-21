using SuperProyecto.Core.Entidades;
using SuperProyecto.Core;
using SuperProyecto.Core.IServices;
using SuperProyecto.Services.Service;
using SuperProyecto.Core.DTO;
using Moq;
using MySqlConnector;
using System.Reflection;

namespace SuperProyecto.Tests;

public class TestAdoSector 
{   
    [Fact]
    public void CuandoHaceUnInsertEnTarifa_DebeAlmacenarDichaFilaEnLaTablaTarifa1()//Crear sector
        {
        var moq = new Mock<ISectorService>();

        Sector sector  = new Sector {  idSector = 1,idLocal =1, nombre="Casimiro" };

        moq.Setup(t => t.AltaSector(SectorDto));
        moq.Setup(t => t.DetalleSector(sector.idSector)).Returns(sector);
        var resultado = moq.Object.DetalleSector(sector.idSector);

        Assert.NotNull(resultado);
        Assert.Equal(sector.idSector, resultado.idSector);
        }

    [Fact]
    public void Cuando_se_agrega_una_nueva_Sector_se_crea_nuevos_valores_de_las_variables()
    {
        
        var moq = new Mock<IsectorService>();
        int idSector = 2;
        int idLocal =2;
        string nombre = "Sector Vip";
        var sector = new Sector { idSector = idSector, idLocal = idLocal, nombre = nombre };

       
        moq.Object.AltaSector(sector);

       
        moq.Verify(r => r.AltaSector(It.Is<Core.DTO.SectorDto>(t =>
            t.idSector == idSector &&
            t.idLocal == idLocal &&
            t.nombre == nombre
        )), Times.Once);
    }
}
 