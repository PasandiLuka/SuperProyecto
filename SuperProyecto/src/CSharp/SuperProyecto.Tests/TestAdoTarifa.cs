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

        Tarifa tarifa = new Tarifa { idTarifa = 1, idFuncion = 1, nombre = "Robertito", precio = 200, stock = 300 };

            moq.Setup(t => t.AltaTarifa(tarifa));
            moq.Setup(t => t.DetalleTarifa(tarifa.idTarifa)).Returns(tarifa);
            var resultado = moq.Object.DetalleTarifa(tarifa.idTarifa);

            Assert.NotNull(resultado);
            Assert.Equal(tarifa.idTarifa, resultado.idTarifa);
        }


            
    [Fact]
    public void CuandoSolicitaTarifasPorFuncion_DebeRetornarListaDeTarifas()
    {
        // Arrange
        var moq = new Mock<IRepoTarifa>();
        int idFuncion = 1;
        var tarifas = new List<Tarifa>
        {
            new Tarifa { idTarifa = 1, idFuncion = idFuncion, nombre = "General", precio = 500, stock = 100 },
            new Tarifa { idTarifa = 2, idFuncion = idFuncion, nombre = "VIP", precio = 1000, stock = 50 }
        };

        moq.Setup(r => r.GetTarifa()).Returns(tarifas);

        var resultado = moq.Object.GetTarifa();

        Assert.NotNull(resultado);
        Assert.Equal(2,((List<Tarifa>)resultado).Count);
        Assert.All(resultado, t => Assert.Equal(idFuncion, t.idFuncion));
    }
}
