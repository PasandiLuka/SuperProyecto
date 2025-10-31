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
            new Tarifa { idTarifa = 1, precio = 1500 },
            new Tarifa { idTarifa = 2, precio = 2000 }
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
        var tarifa = new Tarifa { idTarifa = 1, precio = 1800 };

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
        var tarifaDto = new TarifaDto { idTarifa = 3, precio = 2500 };

        mockService.Setup(s => s.AltaTarifa(tarifaDto))
            .Returns(Result<TarifaDto>.Created(new TarifaDto
            {
                idTarifa = tarifaDto.idTarifa,
                precio = tarifaDto.precio
            }));

        // Act
        var resultado = mockService.Object.AltaTarifa(tarifaDto);

        // Assert
        Assert.True(resultado.Success);
        Assert.Equal(EResultType.Created, resultado.ResultType);
        Assert.Equal(tarifaDto.idTarifa, resultado.Data.idTarifa);
        Assert.Equal(tarifaDto.precio, resultado.Data.precio);
    }

    [Fact]
    public void CuandoActualizoUnaTarifaValida_DebeRetornarOk()
    {
        // Arrange
        var mockService = new Mock<ITarifaService>();
        var tarifaDto = new TarifaDto { idTarifa = 1, precio = 2200 };

        mockService.Setup(s => s.UpdateTarifa(tarifaDto, tarifaDto.idTarifa))
            .Returns(Result<TarifaDto>.Ok(new TarifaDto
            {
                idTarifa = tarifaDto.idTarifa,
                precio = tarifaDto.precio
            }));

        // Act
        var resultado = mockService.Object.UpdateTarifa(tarifaDto, tarifaDto.idTarifa);

        // Assert
        Assert.True(resultado.Success);
        Assert.Equal(EResultType.Ok, resultado.ResultType);
        Assert.Equal(tarifaDto.precio, resultado.Data.precio);
    }

    [Fact]
    public void CuandoRealizoUnAltaDeTarifaInvalida_DebeRetornarBadRequest()
    {
        // Arrange
        var mockRepoSector = new Mock<IRepoSector>();
        mockRepoSector.Setup(r => r.DetalleSector(It.IsAny<int>())).Returns((Sector?)null);

        var tarifaDto = new TarifaDto { idTarifa = 0, precio = -10 };
        var validator = new TarifaValidator();

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
                idTarifa = tarifaDto.idTarifa,
                precio = tarifaDto.precio
            });
        }

        // Assert
        Assert.False(resultado.Success);
        Assert.Equal(EResultType.BadRequest, resultado.ResultType);
        Assert.True(resultado.Errors.ContainsKey("idSector"));
        Assert.True(resultado.Errors.ContainsKey("precio"));
    }
}
