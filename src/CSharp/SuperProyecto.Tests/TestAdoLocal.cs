using SuperProyecto.Core.Entidades;
using SuperProyecto.Core;
using SuperProyecto.Core.IServices;
using SuperProyecto.Services.Service;
using Moq;
using MySqlConnector;

public class TestAdoLocal
{


    //Crea un local

    //Lista de locales
    [Fact]
    public void Retornar_Lista_De_Local()
    {
        var moq = new Mock<ILocalService>();
        List<Local> local = new List<Local>
        {
            new Local{idLocal = 1, nombre = "Teatro Colón", direccion ="Cerrito 628"},
            new Local{idLocal = 2, nombre = "Luna Parck", direccion = "Bouchard 465"}
        };

        moq.Setup(c => c.GetLocales()).Returns(local);
        var resultado = moq.Object.GetLocales();

        Assert.NotEmpty(resultado);
        Assert.Equal(2, ((List<Local>)resultado).Count());
    }

    //Detalle de local
    [Fact]
    public void Retornar_Detalle_Del_Local_Por_Id()
    {
        var moq = new Mock<ILocalService>();
        var id = 1;
        var local = new Local { idLocal = 1, nombre = "Teatro Colón", direccion ="Cerrito 628"};

        moq.Setup(c => c.DetalleLocal(id)).Returns(local);
        var resultado = moq.Object.DetalleLocal(id);

        Assert.NotNull(resultado);
        Assert.Equal(local.idLocal, resultado.idLocal);
        Assert.Equal(local.nombre, resultado.nombre);
        Assert.Equal(local.direccion, resultado.direccion);
    }

    //Actualiza datos del local 

    //Elimina un local (si no tiene funciones vigentes)
}   