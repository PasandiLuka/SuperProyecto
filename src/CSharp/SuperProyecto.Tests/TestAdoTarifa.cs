using SuperProyecto.Core.Enums;
using SuperProyecto.Core.Persistencia;
using SuperProyecto.Services.Validators;

namespace SuperProyecto.Tests;

public class TestAdoTarifa
{
    [Fact]
    public void CuandoObtengoLasTarifas_DebeRetornarUnaListaDeTarifas_ConResultadoOk()
    {
        // Arrange
        var mockService = new Mock<ITarifaService>();
        var tarifas = new List<Tarifa>
        {
            new Tarifa { idTarifa = 1, idFuncion = 1, idSector = 1, precio = 1500, stock = 100 },
            new Tarifa { idTarifa = 2, idFuncion = 1, idSector = 1, precio = 1500, stock = 100 }
        };

        mockService.Setup(s => s.GetTarifas())
            .Returns(Result<IEnumerable<Tarifa>>.Ok(tarifas));

        // Act
        var resultado = mockService.Object.GetTarifas();

        // Assert
        Assert.True(resultado.Success);
        Assert.Equal(EResultType.Ok, resultado.ResultType);
        Assert.Equal(tarifas.Count, resultado.Data.Count());
    }

    [Fact]
    public void CuandoBuscoDetalleDeUnaTarifaValida_DebeRetornarTarifa_ConResultadoOk()
    {
        // Arrange
        var mockService = new Mock<ITarifaService>();
        var tarifa = new Tarifa { idTarifa = 1, idFuncion = 1, idSector = 1, precio = 1500, stock = 100 };

        mockService.Setup(s => s.DetalleTarifa(tarifa.idTarifa))
            .Returns(Result<Tarifa>.Ok(tarifa));

        // Act
        var resultado = mockService.Object.DetalleTarifa(tarifa.idTarifa);

        // Assert
        Assert.True(resultado.Success);
        Assert.Equal(tarifa.idTarifa, resultado.Data.idTarifa);
        Assert.Equal(tarifa.precio, resultado.Data.precio);
    }

    [Fact]
    public void CuandoRealizoUnAltaDeTarifaValida_DebeRetornarCreated()
    {
        // Arrange
        var mockService = new Mock<ITarifaService>();
        var tarifaDto = new TarifaDto { idFuncion = 1, idSector = 1, precio = 1500, stock = 100 };

        mockService.Setup(s => s.AltaTarifa(tarifaDto))
            .Returns(Result<TarifaDto>.Created(new TarifaDto
            {
                idFuncion = tarifaDto.idFuncion,
                idSector = tarifaDto.idSector,
                precio = tarifaDto.precio,
                stock = tarifaDto.stock
            }));

        // Act
        var resultado = mockService.Object.AltaTarifa(tarifaDto);

        // Assert
        Assert.True(resultado.Success);
        Assert.Equal(EResultType.Created, resultado.ResultType);
        Assert.Equal(tarifaDto.precio, resultado.Data.precio);
    }

    [Fact]
    public void CuandoActualizoUnaTarifaValida_DebeRetornarOk()
    {
        // Arrange
        var mockService = new Mock<ITarifaService>();
        var tarifaDto = new TarifaDto { idFuncion = 1, idSector = 1, precio = 1500, stock = 100 };

        mockService.Setup(s => s.UpdateTarifa(tarifaDto, 1))
            .Returns(Result<TarifaDto>.Ok(new TarifaDto
            {
                idFuncion = tarifaDto.idFuncion,
                idSector = tarifaDto.idSector,
                precio = tarifaDto.precio,
                stock = tarifaDto.stock
            }));

        // Act
        var resultado = mockService.Object.UpdateTarifa(tarifaDto, 1);

        // Assert
        Assert.True(resultado.Success);
        Assert.Equal(EResultType.Ok, resultado.ResultType);
        Assert.Equal(tarifaDto.precio, resultado.Data.precio);
    }

    [Fact]
    public void CuandoRealizoUnAltaDeTarifaInvalida_DebeRetornarBadRequest()
    {
        // Arrange
        var mockRepoFuncion = new Mock<IRepoFuncion>();
        var mockRepoSector = new Mock<IRepoSector>();
        var tarifaDto = new TarifaDto { idFuncion = 0, idSector = 0, precio = -1500, stock = -100 };
        var validator = new TarifaValidator(mockRepoFuncion.Object, mockRepoSector.Object);

        // Act
        var validationResult = validator.Validate(tarifaDto);

        Result<Tarifa> resultado;
        if (!validationResult.IsValid)
        {
            var errores = validationResult.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

            resultado = Result<Tarifa>.BadRequest(errores);
        }
        else
        {
            resultado = Result<Tarifa>.Created(new Tarifa
            {
                idTarifa = 0,
                idFuncion = tarifaDto.idFuncion,
                idSector = tarifaDto.idSector,
                precio = tarifaDto.precio,
                stock = tarifaDto.stock
            });
        }

        // Assert
        Assert.False(resultado.Success);
        Assert.Equal(EResultType.BadRequest, resultado.ResultType);
        Assert.True(resultado.Errors.ContainsKey("idFuncion"));
        Assert.True(resultado.Errors.ContainsKey("idSector"));
        Assert.True(resultado.Errors.ContainsKey("precio"));
        Assert.True(resultado.Errors.ContainsKey("stock"));
    }
}
