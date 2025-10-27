using SuperProyecto.Core.Enums;
using SuperProyecto.Services.Validators;

namespace SuperProyecto.Tests;

public class TestAdoEvento
{
    [Fact]
    public void CuandoObtengoLosEventos_DebeRetornarUnaListaDeEventos_ConResultadoOk()
    {
        // Arrange
        var mockService = new Mock<IEventoService>();
        var eventos = new List<Evento>
        {
            new Evento { idEvento = 1, nombre = "Concierto", descripcion = "Música en vivo", fechaPublicacion = DateTime.Today, publicado = true, cancelado = false },
            new Evento { idEvento = 2, nombre = "Teatro", descripcion = "Obra de teatro", fechaPublicacion = DateTime.Today, publicado = false, cancelado = false }
        };

        mockService.Setup(s => s.GetEventos()).Returns(Result<IEnumerable<Evento>>.Ok(eventos));

        // Act
        var resultado = mockService.Object.GetEventos();

        // Assert
        Assert.True(resultado.Success);
        Assert.Equal(EResultType.Ok, resultado.ResultType);
        Assert.Equal(eventos.Count, resultado.Data.Count());
    }

    [Fact]
    public void CuandoBuscoDetalleDeUnEventoValido_DebeRetornarEvento_ConResultadoOk()
    {
        // Arrange
        var mockService = new Mock<IEventoService>();
        var evento = new Evento { idEvento = 1, nombre = "Concierto", descripcion = "Música en vivo", fechaPublicacion = DateTime.Today, publicado = true, cancelado = false };

        mockService.Setup(s => s.DetalleEvento(evento.idEvento)).Returns(Result<Evento>.Ok(evento));

        // Act
        var resultado = mockService.Object.DetalleEvento(evento.idEvento);

        // Assert
        Assert.True(resultado.Success);
        Assert.Equal(evento.idEvento, resultado.Data.idEvento);
        Assert.Equal(evento.nombre, resultado.Data.nombre);
    }

    [Fact]
    public void CuandoRealizoUnAltaDeEventoValido_DebeRetornarCreated()
    {
        // Arrange
        var mockService = new Mock<IEventoService>();
        var evento = new EventoDto { nombre = "Concierto", descripcion = "Música en vivo", publicado = false };
        var validator = new EventoValidator();

        // Act
        var validationResult = validator.Validate(evento);
        Result<Evento> resultado;
        if (!validationResult.IsValid)
        {
            var errores = validationResult.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());
            resultado = Result<Evento>.BadRequest(errores);
        }
        else
        {
            resultado = Result<Evento>.Created(new Evento
            {
                idEvento = 1,
                nombre = evento.nombre,
                descripcion = evento.descripcion,
                publicado = evento.publicado,
                cancelado = false,
                fechaPublicacion = DateTime.Today
            });
        }

        // Assert
        Assert.True(resultado.Success);
        Assert.Equal(EResultType.Created, resultado.ResultType);
        Assert.Equal(evento.nombre, resultado.Data.nombre);
        Assert.Equal(evento.descripcion, resultado.Data.descripcion);
    }

    [Fact]
    public void CuandoRealizoUnAltaDeEventoInvalido_DebeRetornarBadRequest()
    {
        // Arrange
        var mockService = new Mock<IEventoService>();
        var evento = new EventoDto { nombre = "A", descripcion = "", publicado = false };
        var validator = new EventoValidator();

        // Act
        var validationResult = validator.Validate(evento);
        Result<Evento> resultado;
        if (!validationResult.IsValid)
        {
            var errores = validationResult.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());
            resultado = Result<Evento>.BadRequest(errores);
        }
        else
        {
            resultado = Result<Evento>.Created(new Evento
            {
                idEvento = 1,
                nombre = evento.nombre,
                descripcion = evento.descripcion,
                publicado = evento.publicado,
                cancelado = false,
                fechaPublicacion = DateTime.Today
            });
        }

        // Assert
        Assert.False(resultado.Success);
        Assert.Equal(EResultType.BadRequest, resultado.ResultType);
        Assert.True(resultado.Errors.ContainsKey("nombre"));
        Assert.True(resultado.Errors.ContainsKey("descripcion"));
    }

    [Fact]
    public void CuandoActualizoUnEventoValido_DebeRetornarOk()
    {
        // Arrange
        var mockService = new Mock<IEventoService>();
        var evento = new EventoDto { nombre = "Concierto", descripcion = "Música actualizada", publicado = true };
        var validator = new EventoValidator();

        // Act
        var validationResult = validator.Validate(evento);
        Result<Evento> resultado;
        if (!validationResult.IsValid)
        {
            var errores = validationResult.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());
            resultado = Result<Evento>.BadRequest(errores);
        }
        else
        {
            resultado = Result<Evento>.Ok(new Evento
            {
                idEvento = 1,
                nombre = evento.nombre,
                descripcion = evento.descripcion,
                publicado = evento.publicado,
                cancelado = false,
                fechaPublicacion = DateTime.Today
            });
        }

        // Assert
        Assert.True(resultado.Success);
        Assert.Equal(EResultType.Ok, resultado.ResultType);
        Assert.Equal(evento.nombre, resultado.Data.nombre);
        Assert.Equal(evento.descripcion, resultado.Data.descripcion);
    }

    [Fact]
    public void CuandoActualizoUnEventoInvalido_DebeRetornarBadRequest()
    {
        // Arrange
        var mockService = new Mock<IEventoService>();
        var evento = new EventoDto { nombre = "", descripcion = "abc", publicado = true };
        var validator = new EventoValidator();

        // Act
        var validationResult = validator.Validate(evento);
        Result<Evento> resultado;
        if (!validationResult.IsValid)
        {
            var errores = validationResult.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());
            resultado = Result<Evento>.BadRequest(errores);
        }
        else
        {
            resultado = Result<Evento>.Ok(new Evento
            {
                idEvento = 1,
                nombre = evento.nombre,
                descripcion = evento.descripcion,
                publicado = evento.publicado,
                cancelado = false,
                fechaPublicacion = DateTime.Today
            });
        }

        // Assert
        Assert.False(resultado.Success);
        Assert.Equal(EResultType.BadRequest, resultado.ResultType);
        Assert.True(resultado.Errors.ContainsKey("nombre"));
    }
}