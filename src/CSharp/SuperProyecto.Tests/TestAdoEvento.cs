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
    public void d()//Crear evento
        {
            var moq = new Mock<IEventoService>();
            Evento evento = new Evento { idEvento = 1, nombre = "Sofi", descripcion = "chiquita",fechaPublicacion= DateTime.Now,publicado=true,cancelado=false };

            moq.Setup(t => t.DetalleEvento(evento.idEvento)).Returns(evento);
            // GetEventos debe devolver una colección
            moq.Setup(t => t.GetEventos()).Returns(new List<Evento> { evento });
            var resultado = moq.Object.DetalleEvento(evento.idEvento);

            Assert.NotNull(resultado);
            Assert.Equal(evento.idEvento, resultado.idEvento);
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



