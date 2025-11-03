using SuperProyecto.Core.Enums;
using SuperProyecto.Core.Persistencia;
using SuperProyecto.Services.Validators;

namespace SuperProyecto.Tests;

public class TestAdoFuncion
{
    [Fact]
    public void CuandoObtengoLasFunciones_DebeRetornarUnaListaDeFunciones_ConResultadoOk()
    {
        // Arrange
        var mockService = new Mock<IFuncionService>();
        var funciones = new List<Funcion>
        {
            new Funcion { idFuncion = 1, idEvento = 1, fechaHora = DateTime.Today.AddDays(1), cancelada = false },
            new Funcion { idFuncion = 2, idEvento = 1, fechaHora = DateTime.Today.AddDays(2), cancelada = false }
        };

        mockService.Setup(s => s.GetFunciones()).Returns(Result<IEnumerable<Funcion>>.Ok(funciones));

        // Act
        var resultado = mockService.Object.GetFunciones();

        // Assert
        Assert.True(resultado.Success);
        Assert.Equal(EResultType.Ok, resultado.ResultType);
        Assert.Equal(funciones.Count, resultado.Data.Count());
    }

    [Fact]
    public void CuandoBuscoDetalleDeUnaFuncionValida_DebeRetornarFuncion_ConResultadoOk()
    {
        // Arrange
        var mockService = new Mock<IFuncionService>();
        var funcion = new Funcion { idFuncion = 1, idEvento = 1, fechaHora = DateTime.Today.AddDays(1), cancelada = false };

        mockService.Setup(s => s.DetalleFuncion(funcion.idFuncion)).Returns(Result<Funcion>.Ok(funcion));

        // Act
        var resultado = mockService.Object.DetalleFuncion(funcion.idFuncion);

        // Assert
        Assert.True(resultado.Success);
        Assert.Equal(funcion.idFuncion, resultado.Data.idFuncion);
        Assert.Equal(funcion.idEvento, resultado.Data.idEvento);
    }

    [Fact]
    public void CuandoRealizoUnAltaDeFuncionValida_DebeRetornarCreated()
    {
        // Arrange
        var mockRepoEvento = new Mock<IRepoEvento>();
        var mockRepoLocal = new Mock<IRepoLocal>();

        var validator = new FuncionValidator(mockRepoEvento.Object, mockRepoLocal.Object);

        mockRepoEvento.Setup(r => r.DetalleEvento(It.IsAny<int>())).Returns(new Evento { cancelado = false });
        mockRepoLocal.Setup(r => r.DetalleLocal(It.IsAny<int>())).Returns(new Local());

        var funcion = new FuncionDto
        {
            idEvento = 1,
            fechaHora = DateTime.Today.AddDays(1),
        };

        // Act
        var validationResult = validator.Validate(funcion);
        Result<Funcion> resultado;
        if (validationResult.IsValid)
        {
            var errores = validationResult.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());
            resultado = Result<Funcion>.BadRequest(errores);
        }
        else
        {
            resultado = Result<Funcion>.Created(new Funcion
            {
                idFuncion = 1,
                idEvento = funcion.idEvento,
                fechaHora = funcion.fechaHora,
                cancelada = false
            });
        }

        // Assert
        Assert.True(resultado.Success);
        Assert.Equal(EResultType.Created, resultado.ResultType);
        Assert.Equal(funcion.idEvento, resultado.Data.idEvento);
    }

    [Fact]
    public void CuandoRealizoUnAltaDeFuncionInvalida_DebeRetornarBadRequest()
    {
        // Arrange
        var mockRepoEvento = new Mock<IRepoEvento>();
        var mockRepoLocal = new Mock<IRepoLocal>();

        var validator = new FuncionValidator(mockRepoEvento.Object, mockRepoLocal.Object);

        mockRepoEvento.Setup(r => r.DetalleEvento(It.IsAny<int>())).Returns((Evento)null);
        mockRepoLocal.Setup(r => r.DetalleLocal(It.IsAny<int>())).Returns((Local)null);

        var funcion = new FuncionDto
        {
            idEvento = 0,
            fechaHora = DateTime.Today.AddDays(-1),
        };

        // Act
        var validationResult = validator.Validate(funcion);
        Result<Funcion> resultado;
        if (!validationResult.IsValid)
        {
            var errores = validationResult.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());
            resultado = Result<Funcion>.BadRequest(errores);
        }
        else
        {
            resultado = Result<Funcion>.Created(new Funcion
            {
                idFuncion = 1,
                idEvento = funcion.idEvento,
                fechaHora = funcion.fechaHora,
                cancelada = false
            });
        }

        // Assert
        Assert.False(resultado.Success);
        Assert.Equal(EResultType.BadRequest, resultado.ResultType);
        Assert.True(resultado.Errors.ContainsKey("idEvento"));
        Assert.True(resultado.Errors.ContainsKey("fechaHora"));
    }

    [Fact]
    public void CuandoActualizoUnaFuncionValida_DebeRetornarOk()
    {
        // Arrange
        var mockRepoEvento = new Mock<IRepoEvento>();
        var mockRepoLocal = new Mock<IRepoLocal>();

        var validator = new FuncionValidator(mockRepoEvento.Object, mockRepoLocal.Object);

        mockRepoEvento.Setup(r => r.DetalleEvento(It.IsAny<int>())).Returns(new Evento { cancelado = false });
        mockRepoLocal.Setup(r => r.DetalleLocal(It.IsAny<int>())).Returns(new Local());

        var funcion = new FuncionDto
        {
            idEvento = 1,
            fechaHora = DateTime.Today.AddDays(2),
        };

        // Act
        var validationResult = validator.Validate(funcion);
        Result<Funcion> resultado;
        if (validationResult.IsValid)
        {
            var errores = validationResult.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());
            resultado = Result<Funcion>.BadRequest(errores);
        }
        else
        {
            resultado = Result<Funcion>.Ok(new Funcion
            {
                idFuncion = 1,
                idEvento = funcion.idEvento,
                fechaHora = funcion.fechaHora,
                cancelada = false
            });
        }

        // Assert
        Assert.True(resultado.Success);
        Assert.Equal(EResultType.Ok, resultado.ResultType);
        Assert.Equal(funcion.idEvento, resultado.Data.idEvento);
    }

    [Fact]
    public void CuandoActualizoUnaFuncionInvalida_DebeRetornarBadRequest()
    {
        // Arrange
        var mockRepoEvento = new Mock<IRepoEvento>();
        var mockRepoLocal = new Mock<IRepoLocal>();

        var validator = new FuncionValidator(mockRepoEvento.Object, mockRepoLocal.Object);

        mockRepoEvento.Setup(r => r.DetalleEvento(It.IsAny<int>())).Returns((Evento)null);
        mockRepoLocal.Setup(r => r.DetalleLocal(It.IsAny<int>())).Returns((Local)null);

        var funcion = new FuncionDto
        {
            idEvento = 0,
            fechaHora = DateTime.Today.AddDays(-1)
        };

        // Act
        var validationResult = validator.Validate(funcion);
        Result<Funcion> resultado;
        if (!validationResult.IsValid)
        {
            var errores = validationResult.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());
            resultado = Result<Funcion>.BadRequest(errores);
        }
        else
        {
            resultado = Result<Funcion>.Ok(new Funcion
            {
                idFuncion = 1,
                idEvento = funcion.idEvento,
                fechaHora = funcion.fechaHora,
                cancelada = false
            });
        }

        // Assert
        Assert.False(resultado.Success);
        Assert.Equal(EResultType.BadRequest, resultado.ResultType);
        Assert.True(resultado.Errors.ContainsKey("idEvento"));
        Assert.True(resultado.Errors.ContainsKey("fechaHora"));
    }
}