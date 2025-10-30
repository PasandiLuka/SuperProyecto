using SuperProyecto.Core.Enums;
using SuperProyecto.Core.Persistencia;
using SuperProyecto.Services.Validators;

namespace SuperProyecto.Tests;

public class TestAdoEntrada
{
    [Fact]
    public void Testea_De_que_se_alla_creado_la_lista_de_entrada()
    {
        // Arrange
        var mockService = new Mock<IEntradaService>();
        var entrada = new Entrada { idEntrada = 1, idOrden = 101, idQr = 50, usada = true};
        
       
        mockService.Setup(s => s.DetalleEntrada(entrada.idEntrada))
            .Returns(Result<Entrada>.Ok(entrada));

        //act   
        var resultado = mockService.Object.DetalleEntrada(entrada.idEntrada);
        
        // Assert
        Assert.True(resultado.Success);
        Assert.Equal(entrada.idEntrada, resultado.Data.idEntrada);
        Assert.Equal(entrada.idOrden, resultado.Data.idOrden);
        Assert.Equal(entrada.idQr, resultado.Data.idQr);
        Assert.Equal(entrada.usada, resultado.Data.usada);
    }
}