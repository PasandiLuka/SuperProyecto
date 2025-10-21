using Moq;
using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.IServices;
using SuperProyecto.Services.Service;
using MySqlConnector;
using SuperProyecto.Core;

namespace SuperProyecto.Tests;

public class TestAdoTarifa
{

    [Fact]
    public void CuandoHaceUnInsertEnTarifa_DebeAlmacenarDichaFilaEnLaTablaTarifa1()//Crea tarifa
    {
        var moq = new Mock<ITarifaService>();
        var tarifa = new Tarifa { idTarifa = 1, idSector = 1, precio = 200 };


        moq.Setup(t => t.DetalleTarifa(tarifa.idTarifa)).Returns(tarifa);
        moq.Setup (t => t.GetTarifas());

        var resultado = moq.Object.DetalleTarifa(tarifa.idTarifa);

        Assert.NotNull(resultado);
        Assert.Equal(tarifa.idTarifa, resultado.idTarifa);
        Assert.Equal(tarifa.idSector, resultado.idSector);
        Assert.Equal(tarifa.precio, resultado.precio);
    }




    [Fact]
    public void CuandoSolicitaTarifasPorFuncion_DebeRetornarListaDeTarifas()//Listar tarifas
    {
        // Arrange
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

    [Fact]
    public void Cuando_se_agrega_una_nueva_tarifa_se_crea_nuevos_valores_de_las_variables()//Update tarifa
    {
        // Arrange
        var moq = new Mock<ITarifaService>();
        int idtarifa = 10;
        int idsector = 2;
        int precioo = 1500;
        var tarifa = new Core.DTO.TarifaDto { idTarifa = idtarifa, idSector = idsector, precio = precioo };

        // Act
        moq.Object.AltaTarifa(tarifa);

        // Assert
        moq.Verify(r => r.AltaTarifa(It.Is<Core.DTO.TarifaDto>(t =>
            t.idTarifa == idtarifa &&
            t.idSector == idsector &&
            t.precio == precioo
        )), Times.Once);
    }
}

