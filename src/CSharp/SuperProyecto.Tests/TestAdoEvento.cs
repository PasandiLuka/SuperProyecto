using Moq;
using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.IServices;
using SuperProyecto.Services.Service;
using MySqlConnector;
using SuperProyecto.Core;


namespace SuperProyecto.Tests;

public class TestAdoEvento
{
    [Fact]
    public void CuandoHaceUnInsertEnEvento_DebeAlmacenarDichaFilaEnLaTablaEvento()//Crear evento
        {
            var moq = new Mock<IEventoService>();
            Evento evento = new Evento { idEvento = 1, nombre = "Sofi", descripcion = "chiquita",fechaPublicacion= DateTime.Now,publicado=true,cancelado=false };

            moq.Setup(t => t.AltaEvento(tarifa));
            moq.Setup(t => t.DetalleTarifa(tarifa.idTarifa)).Returns(tarifa);
            var resultado = moq.Object.DetalleTarifa(tarifa.idTarifa);

            Assert.NotNull(resultado);
            Assert.Equal(tarifa.idTarifa, resultado.idTarifa);
            
        }



    [Fact]
    public void CuandoSolicitaEventosPorFuncion_DebeRetornarListaDeEvento()//muestra lista de eventos
    {

        var moq = new Mock<IEventoService>();
        int idEvento = 1;
        var eventos = new List<Evento>
        {
            new Evento { idEvento = 1, nombre = "Sofi", descripcion = "chiquita",fechaPublicacion= DateTime.Now,publicado=true,cancelado=false},
            new Evento { idEvento = 2, nombre = "Luka", descripcion = "1000" ,fechaPublicacion=DateTime.Now,publicado=true,cancelado=false}
        };

        moq.Setup(r => r.GetEventos()).Returns(eventos);

        var resultado = moq.Object.GetEventos();

        Assert.NotNull(resultado);
        Assert.Equal(2, ((List<Evento>)resultado).Count);
        Assert.Contains(resultado, e => e.idEvento == idEvento);
    }
}



