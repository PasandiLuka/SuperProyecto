using SuperProyecto.Core.Entidades;
using SuperProyecto.Core;
using SuperProyecto.Core.IServices;
using SuperProyecto.Services.Service;
using SuperProyecto.Core.DTO;
using Moq;
using MySqlConnector;
using System.Reflection;

namespace SuperProyecto.Tests;

public class TestAdoOrden
{
     //Lista de Ordenes
    [Fact]
    public void Retornar_Lista_De_Ordenes()
    {
        var moq = new Mock<IOrdenService>();
        List<Orden> orden = new List<Orden>
        {
            new Orden{idOrden = 1, DNI = 12235, idFuncion = 1, fecha = DateTime.Now, pagada = true },
            new Orden{idOrden = 1, DNI = 3574, idFuncion = 1, fecha = DateTime.Now, pagada = true }
        };

        moq.Setup(c => c.GetOrdenes()).Returns(orden);
        var resultado = moq.Object.GetOrdenes();

        Assert.NotEmpty(resultado);
        Assert.Equal(2, ((List<Orden>)resultado).Count());
    }

    //Detalle de un Orden
    [Fact]
    public void Retornar_Detalle_De_Una_Orden()
    {
        var moq = new Mock<IOrdenService>();
        var id = 1;
        var orden = new Orden{idOrden = 1,DNI = 123435,idFuncion = 1,fecha = DateTime.Now,pagada = true};

        moq.Setup(c => c.DetalleOrden(id)).Returns(orden);

        var resultado = moq.Object.DetalleOrden(id);

        Assert.NotNull(resultado);
        Assert.Equal(orden.idOrden, resultado.idOrden);
        Assert.Equal(orden.DNI, resultado.DNI);
        Assert.Equal(orden.idFuncion, resultado.idFuncion);
        Assert.Equal(orden.fecha, resultado.fecha);
        Assert.Equal(orden.pagada, resultado.pagada);
    }

}
