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

        var sector  = new Sector {  idSector = 1,idLocal =1, nombre="Casimiro" };

        moq.Setup(t => t.DetalleSector(sector.idSector)).Returns(sector);
        moq.Setup (t => t.GetSectores());

        var resultado = moq.Object.DetalleSector(sector.idSector);

        Assert.NotNull(resultado);
        Assert.Equal(sector.idSector, resultado.idSector);
        }

    [Fact]
    public void Cuando_se_agrega_una_nueva_Sector_se_crea_nuevos_valores_de_las_variables()//muestra la lista de sectores
    {
        

        var moq = new Mock<ITarifaService>();
        var tarifas = new List<Tarifa>
        {
            new Tarifa { idTarifa = 1, idSector = 1, precio = 500},
            new Tarifa { idTarifa = 2, idSector = 2, precio = 1000 }
        };

        moq.Setup(r => r.DetalleTarifa(1));

        var resultado = moq.Object.DetalleTarifa(1);

        Assert.NotNull(resultado);
        Assert.Equal(2,tarifas.Count);
    }
}
 