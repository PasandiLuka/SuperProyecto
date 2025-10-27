using SuperProyecto.Core.Enums;
using SuperProyecto.Services.Validators;

namespace SuperProyecto.Tests;

public class TestAdoLocal
{
    [Fact]
    public void CuandoObtengoLosLocales_DebeRetornarUnaListaDeLocales_ConResultadoOk()
    {
        // Arrange
        var mockService = new Mock<ILocalService>();
        var locales = new List<Local>
        {
            new Local { idLocal = 1, nombre = "Teatro Principal", direccion = "Calle Falsa 123" },
            new Local { idLocal = 2, nombre = "Sala Alternativa", direccion = "Avenida Siempre Viva 456" }
        };

        mockService.Setup(s => s.GetLocales()).Returns(Result<IEnumerable<Local>>.Ok(locales));

        // Act
        var resultado = mockService.Object.GetLocales();

        // Assert
        Assert.True(resultado.Success);
        Assert.Equal(EResultType.Ok, resultado.ResultType);
        Assert.Equal(locales.Count, resultado.Data.Count());
    }

    [Fact]
    public void CuandoBuscoDetalleDeUnLocalValido_DebeRetornarLocal_ConResultadoOk()
    {
        // Arrange
        var mockService = new Mock<ILocalService>();
        var local = new Local { idLocal = 1, nombre = "Teatro Principal", direccion = "Calle Falsa 123" };

        mockService.Setup(s => s.DetalleLocal(local.idLocal)).Returns(Result<Local>.Ok(local));

        // Act
        var resultado = mockService.Object.DetalleLocal(local.idLocal);

        // Assert
        Assert.True(resultado.Success);
        Assert.Equal(local.idLocal, resultado.Data.idLocal);
        Assert.Equal(local.nombre, resultado.Data.nombre);
    }

    [Fact]
    public void CuandoRealizoUnAltaDeLocalValido_DebeRetornarCreated()
    {
        // Arrange
        var validator = new LocalValidator();
        var local = new LocalDto { nombre = "Sala Nueva", direccion = "Calle 9 de Julio 100" };

        // Act
        var validationResult = validator.Validate(local);
        Result<Local> resultado;
        if (!validationResult.IsValid)
        {
            var errores = validationResult.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());
            resultado = Result<Local>.BadRequest(errores);
        }
        else
        {
            resultado = Result<Local>.Created(new Local
            {
                idLocal = 3,
                nombre = local.nombre,
                direccion = local.direccion
            });
        }

        // Assert
        Assert.True(resultado.Success);
        Assert.Equal(EResultType.Created, resultado.ResultType);
        Assert.Equal(local.nombre, resultado.Data.nombre);
        Assert.Equal(local.direccion, resultado.Data.direccion);
    }

    [Fact]
    public void CuandoRealizoUnAltaDeLocalInvalido_DebeRetornarBadRequest()
    {
        // Arrange
        var validator = new LocalValidator();
        var local = new LocalDto { nombre = "Sa", direccion = "" };

        // Act
        var validationResult = validator.Validate(local);
        Result<Local> resultado;
        if (!validationResult.IsValid)
        {
            var errores = validationResult.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());
            resultado = Result<Local>.BadRequest(errores);
        }
        else
        {
            resultado = Result<Local>.Created(new Local
            {
                idLocal = 1,
                nombre = local.nombre,
                direccion = local.direccion
            });
        }

        // Assert
        Assert.False(resultado.Success);
        Assert.Equal(EResultType.BadRequest, resultado.ResultType);
        Assert.True(resultado.Errors.ContainsKey("nombre"));
        Assert.True(resultado.Errors.ContainsKey("direccion"));
    }

    [Fact]
    public void CuandoActualizoUnLocalValido_DebeRetornarOk()
    {
        // Arrange
        var validator = new LocalValidator();
        var local = new LocalDto { nombre = "Sala Modificada", direccion = "Nueva Dirección 200" };

        // Act
        var validationResult = validator.Validate(local);
        Result<Local> resultado;
        if (!validationResult.IsValid)
        {
            var errores = validationResult.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());
            resultado = Result<Local>.BadRequest(errores);
        }
        else
        {
            resultado = Result<Local>.Ok(new Local
            {
                idLocal = 1,
                nombre = local.nombre,
                direccion = local.direccion
            });
        }

        // Assert
        Assert.True(resultado.Success);
        Assert.Equal(EResultType.Ok, resultado.ResultType);
        Assert.Equal(local.nombre, resultado.Data.nombre);
        Assert.Equal(local.direccion, resultado.Data.direccion);
    }

    [Fact]
    public void CuandoActualizoUnLocalInvalido_DebeRetornarBadRequest()
    {
        // Arrange
        var validator = new LocalValidator();
        var local = new LocalDto { nombre = "", direccion = "Di" };

        // Act
        var validationResult = validator.Validate(local);
        Result<Local> resultado;
        if (!validationResult.IsValid)
        {
            var errores = validationResult.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());
            resultado = Result<Local>.BadRequest(errores);
        }
        else
        {
            resultado = Result<Local>.Ok(new Local
            {
                idLocal = 1,
                nombre = local.nombre,
                direccion = local.direccion
            });
        }

        // Assert
        Assert.False(resultado.Success);
        Assert.Equal(EResultType.BadRequest, resultado.ResultType);
        Assert.True(resultado.Errors.ContainsKey("nombre"));
        Assert.True(resultado.Errors.ContainsKey("direccion"));
    }
}