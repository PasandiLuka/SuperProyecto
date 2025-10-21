using SuperProyecto.Core.Entidades;
using SuperProyecto.Core;
using SuperProyecto.Core.IServices;
using SuperProyecto.Services.Service;
using Moq;
using MySqlConnector;

namespace SuperProyecto.Tests;

public class TestAdoFuncion
{
    //Crea una Función
    //Lista funciones
    [Fact]
    public void Retornar_Lista_De_Funciones()
    {
        var moq = new Mock<IFuncionService>();
        List<Funcion> funciones = new List<Funcion>
        {
            new Funcion{idFuncion = 1, idEvento = 1, idTarifa = 1, fechaHora = DateTime.Now, stock = 150, cancelada = false },
            new Funcion{idFuncion = 2, idEvento = 2, idTarifa = 2, fechaHora = DateTime.Now, stock = 100, cancelada = false }
        };

        moq.Setup(c => c.GetFunciones()).Returns(funciones);
        var resultado = moq.Object.GetFunciones();

        Assert.NotEmpty(resultado);
        Assert.Equal(2, ((List<Funcion>)resultado).Count());
    }

    //Detalle de función
    [Fact]
    public void Retornar_Detalle_De_Funcion()
    {
        var moq = new Mock<IFuncionService>();
        var id = 1;
        var funcion = new Funcion { idFuncion = 1, idEvento = 1, idTarifa = 1, fechaHora = DateTime.Now, stock = 100, cancelada = false };

        moq.Setup(c => c.DetalleFuncion(id)).Returns(funcion);
        var resultado = moq.Object.DetalleFuncion(id);

        Assert.NotNull(resultado);
        Assert.Equal(funcion.idFuncion, resultado.idFuncion);
        Assert.Equal(funcion.idEvento, resultado.idEvento);
        Assert.Equal(funcion.idTarifa, resultado.idTarifa);
        Assert.Equal(funcion.fechaHora, resultado.fechaHora);
        Assert.Equal(funcion.stock, resultado.stock);
        Assert.Equal(funcion.cancelada, resultado.cancelada);
    }
    //Actualiza función
    //Cancela la función
}