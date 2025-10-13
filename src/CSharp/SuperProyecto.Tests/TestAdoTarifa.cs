using Moq;
using SuperProyecto.Core;
using SuperProyecto.Core.Persistencia;
using SuperProyecto.Dapper;
using MySqlConnector;
using SuperProyecto.Core;

namespace SuperProyecto.Tests;

public class TestAdoTarifa
{
    [Fact]
    public void CuandoHaceUnInsertEnTarifa_DebeAlmacenarDichaFilaEnLaTablaTarifa1()
    {
        var moq = new Mock<IRepoTarifa>();

        Tarifa tarifa = new Tarifa { idTarifa = 1, idSector = 1, precio = 200};

        moq.Setup(t => t.AltaTarifa(tarifa));
        moq.Setup(t => t.DetalleTarifa(tarifa.idTarifa)).Returns(tarifa);
        var resultado = moq.Object.DetalleTarifa(tarifa.idTarifa);

        Assert.NotNull(resultado);
        Assert.Equal(tarifa.idTarifa, resultado.idTarifa);
        Assert.Equal(tarifa.idSector, resultado.idSector);
        Assert.Equal(tarifa.precio, resultado.precio);
    }


            
    [Fact]
    public void CuandoSolicitaTarifasPorFuncion_DebeRetornarListaDeTarifas()
    {
        // Arrange
        var moq = new Mock<IRepoTarifa>();
        int idSector = 1;
        var tarifas = new List<Tarifa>
        {
            new Tarifa { idTarifa = 1, idSector = idSector, precio = 500},
            new Tarifa { idTarifa = 2, idSector = idSector, precio = 1000 }
        };

        moq.Setup(r => r.GetTarifa()).Returns(tarifas);

        var resultado = moq.Object.GetTarifa();

        Assert.NotNull(resultado);
        Assert.Equal(2,((List<Sector>)resultado).Count);
    }
    
    [Fact]
    public void Cuando_se_agrega_una_nueva_tarifa_se_crea_nuevos_valores_de_las_variables()
    {
        // Arrange
        var moq = new Mock<IRepoTarifa>();
        int idtarifa = 10;
        int idsector = 2;
        int precioo = 1500;
        var tarifa = new Tarifa { idTarifa = idtarifa, idSector = idsector, precio = precioo };

        // Act
        moq.Object.AltaTarifa(tarifa);

        // Assert
        moq.Verify(r => r.AltaTarifa(It.Is<Tarifa>(t =>
            t.idTarifa == idtarifa &&
            t.idSector == idsector &&
            t.precio == precioo
        )), Times.Once);
    }

}

