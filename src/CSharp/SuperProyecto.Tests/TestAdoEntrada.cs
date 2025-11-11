namespace SuperProyecto.Tests;

public class TestAdoEntrada
{

    //Lista entradas
    [Fact]
    public void Retornar_Lista_De_Entradas()
    {
        var moq = new Mock<IEntradaService>();
        List<Entrada> entrada = new List<Entrada>
        {
            new Entrada{idEntrada = 1, idOrden = 1, usada = true},
            new Entrada{idEntrada = 2, idOrden = 2, usada = true}
        };

        moq.Setup(c => c.GetEntradas()).Returns(Result<IEnumerable<Entrada>>.Ok(entrada));
        var resultado = moq.Object.GetEntradas();

        Assert.NotEmpty(resultado.Data);
        Assert.Equal(2, resultado.Data.Count());
    }

    //Detalle de una entrada
    [Fact]
    public void Retornar_Detalle_Del_Local_Por_Id()
    {
        var moq = new Mock<IEntradaService>();
        var id = 1;
        var entrada = new Entrada { idEntrada = 1, idOrden = 1, usada = true };

        moq.Setup(c => c.DetalleEntrada(id)).Returns(Result<Entrada>.Ok(entrada));
        var resultado = moq.Object.DetalleEntrada(id);

        Assert.NotNull(resultado);
        Assert.Equal(entrada.idOrden, resultado.Data.idOrden);
        Assert.Equal(entrada.idTarifa, resultado.Data.idTarifa);
        Assert.Equal(entrada.usada, resultado.Data.usada);
    }

    //Cancela una entrada
    [Fact]
    public void Debe_cancelar_una_entrada()
    {
        //arrange
        var moq = new Mock<IEntradaService>();
        var id = 1;
        var entrada = new Entrada { idEntrada = 1, idOrden = 1, usada = true, anulada = true };

        //act
        moq.Setup(c => c.CancelarEntrada(id)).Returns(Result<Entrada>.Ok(entrada));
        var resultado = moq.Object.CancelarEntrada(id);

        //Assert
        Assert.NotNull(resultado);
        Assert.Equal(entrada.anulada, resultado.Data.anulada);
    }
    
}