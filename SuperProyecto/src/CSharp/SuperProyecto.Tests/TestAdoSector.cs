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
        var moq = new Mock<IRepoTarifa>();

        Tarifa tarifa = new Tarifa { idTarifa = 1, idFuncion = 1, nombre = "Robertito", precio = 200, stock = 300 };

        moq.Setup(t => t.AltaTarifa(tarifa));
        moq.Setup(t => t.DetalleTarifa(tarifa.idTarifa)).Returns(tarifa);
        var resultado = moq.Object.DetalleTarifa(tarifa.idTarifa);

        Assert.NotNull(resultado);
        Assert.Equal(tarifa.idTarifa, resultado.idTarifa);
        }


}
 