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
        int idFuncion = 1;

        var tarifas = new List<Tarifa>
        {
            new Tarifa { idTarifa = 1, idFuncion = idFuncion, idSector = 1, precio = 100, stock = 50, activo = true },
            new Tarifa { idTarifa = 2, idFuncion = idFuncion, idSector = 2, precio = 200, stock = 30, activo = true }
        };

        mockService.Setup(s => s.GetTarifas(idFuncion))
            .Returns(Result<IEnumerable<Tarifa>>.Ok(tarifas));

        // Act
        var resultado = mockService.Object.GetTarifas(idFuncion);

        // Assert
        Assert.True(resultado.Success);
        Assert.Equal(EResultType.Ok, resultado.ResultType);
        Assert.Equal(tarifas.Count, resultado.Data.Count());
    }

    [Fact]
    public void CuandoObtengoDetalleDeTarifaValida_DebeRetornarTarifa_ConResultadoOk()
    {
        // Arrange
        var mockService = new Mock<ITarifaService>();
        int idTarifa = 10;

        var tarifa = new Tarifa
        {
            idTarifa = idTarifa,
            idFuncion = 1,
            idSector = 2,
            precio = 150,
            stock = 20,
            activo = true
        };

        mockService.Setup(s => s.DetalleTarifa(idTarifa))
            .Returns(Result<Tarifa?>.Ok(tarifa));

        // Act
        var resultado = mockService.Object.DetalleTarifa(idTarifa);

        // Assert
        Assert.True(resultado.Success);
        Assert.Equal(EResultType.Ok, resultado.ResultType);
        Assert.Equal(tarifa.idTarifa, resultado.Data.idTarifa);
        Assert.Equal(tarifa.precio, resultado.Data.precio);
        Assert.Equal(tarifa.stock, resultado.Data.stock);
        Assert.Equal(tarifa.activo, resultado.Data.activo);
    }

    [Fact]
    public void CuandoObtengoDetalleDeTarifaInexistente_DebeRetornarNotFound()
    {
        // Arrange
        var mockService = new Mock<ITarifaService>();
        int idTarifa = 99;

        mockService.Setup(s => s.DetalleTarifa(idTarifa))
            .Returns(Result<Tarifa?>.NotFound("Tarifa no encontrada"));

        // Act
        var resultado = mockService.Object.DetalleTarifa(idTarifa);

        // Assert
        Assert.False(resultado.Success);
        Assert.Equal(EResultType.NotFound, resultado.ResultType);
        Assert.Null(resultado.Data);
    }

    [Fact]
    public void CuandoDoyDeAltaTarifaValida_DebeRetornarCreated()
    {
        // Arrange
        var mockRepoFuncion = new Mock<IRepoFuncion>();
        var mockRepoSector = new Mock<IRepoSector>();

        var dto = new TarifaDtoAlta
        {
            idFuncion = 1,
            idSector = 2,
            precio = 120,
            stock = 50,
            activo = true
        };

        mockRepoFuncion.Setup(r => r.DetalleFuncion(dto.idFuncion))
            .Returns(new Funcion { idFuncion = dto.idFuncion, cancelada = false });

        mockRepoSector.Setup(r => r.DetalleSector(dto.idSector))
            .Returns(new Sector { idSector = dto.idSector });

        var validator = new TarifaValidator(mockRepoFuncion.Object, mockRepoSector.Object);
        var validation = validator.Validate(dto);

        var mockService = new Mock<ITarifaService>();
        mockService.Setup(s => s.AltaTarifa(dto))
            .Returns(Result<TarifaDto>.Created(dto));

        // Act
        var resultado = mockService.Object.AltaTarifa(dto);

        // Assert
        Assert.True(validation.IsValid);
        Assert.True(resultado.Success);
        Assert.Equal(EResultType.Created, resultado.ResultType);
        Assert.Equal(dto.precio, resultado.Data.precio);
        Assert.Equal(dto.stock, resultado.Data.stock);
        Assert.Equal(dto.activo, resultado.Data.activo);
    }

    [Fact]
    public void CuandoDoyDeAltaTarifaInvalida_DebeRetornarBadRequest()
    {
        // Arrange
        var mockRepoFuncion = new Mock<IRepoFuncion>();
        var mockRepoSector = new Mock<IRepoSector>();

        var dto = new TarifaDtoAlta
        {
            idFuncion = 0, // inválido
            idSector = 0,  // inválido
            precio = -10,  // inválido
            stock = -5,    // inválido
            activo = true
        };

        mockRepoFuncion.Setup(r => r.DetalleFuncion(dto.idFuncion))
            .Returns((Funcion)null);

        mockRepoSector.Setup(r => r.DetalleSector(dto.idSector))
            .Returns((Sector)null);

        var validator = new TarifaValidator(mockRepoFuncion.Object, mockRepoSector.Object);
        var validation = validator.Validate(dto);

        var mockService = new Mock<ITarifaService>();
        mockService.Setup(s => s.AltaTarifa(dto))
            .Returns(Result<TarifaDto>.BadRequest(validation.ToDictionary()));

        // Act
        var resultado = mockService.Object.AltaTarifa(dto);

        // Assert
        Assert.False(validation.IsValid);
        Assert.False(resultado.Success);
        Assert.Equal(EResultType.BadRequest, resultado.ResultType);
        Assert.NotNull(resultado.Errors);
    }

    [Fact]
    public void CuandoActualizoTarifaValida_DebeRetornarOk()
    {
        // Arrange
        var mockRepoFuncion = new Mock<IRepoFuncion>();
        var mockRepoSector = new Mock<IRepoSector>();

        var dto = new TarifaDtoAlta
        {
            idFuncion = 1,
            idSector = 2,
            precio = 200,
            stock = 40,
            activo = true
        };

        int idTarifa = 10;

        mockRepoFuncion.Setup(r => r.DetalleFuncion(dto.idFuncion))
            .Returns(new Funcion { idFuncion = dto.idFuncion, cancelada = false });

        mockRepoSector.Setup(r => r.DetalleSector(dto.idSector))
            .Returns(new Sector { idSector = dto.idSector });

        var validator = new TarifaValidator(mockRepoFuncion.Object, mockRepoSector.Object);
        var validation = validator.Validate(dto);

        var mockService = new Mock<ITarifaService>();
        mockService.Setup(s => s.UpdateTarifa(dto, idTarifa))
            .Returns(Result<TarifaDto>.Ok(dto));

        // Act
        var resultado = mockService.Object.UpdateTarifa(dto, idTarifa);

        // Assert
        Assert.True(validation.IsValid);
        Assert.True(resultado.Success);
        Assert.Equal(EResultType.Ok, resultado.ResultType);
        Assert.Equal(dto.precio, resultado.Data.precio);
        Assert.Equal(dto.stock, resultado.Data.stock);
        Assert.Equal(dto.activo, resultado.Data.activo);
    }

    [Fact]
    public void CuandoActualizoTarifaInvalida_DebeRetornarBadRequest()
    {
        // Arrange
        var mockRepoFuncion = new Mock<IRepoFuncion>();
        var mockRepoSector = new Mock<IRepoSector>();

        var dto = new TarifaDtoAlta
        {
            idFuncion = 0,
            idSector = 0,
            precio = -50,
            stock = -10,
            activo = true
        };

        int idTarifa = 5;

        mockRepoFuncion.Setup(r => r.DetalleFuncion(dto.idFuncion))
            .Returns((Funcion)null);

        mockRepoSector.Setup(r => r.DetalleSector(dto.idSector))
            .Returns((Sector)null);

        var validator = new TarifaValidator(mockRepoFuncion.Object, mockRepoSector.Object);
        var validation = validator.Validate(dto);

        var mockService = new Mock<ITarifaService>();
        mockService.Setup(s => s.UpdateTarifa(dto, idTarifa))
            .Returns(Result<TarifaDto>.BadRequest(validation.ToDictionary()));

        // Act
        var resultado = mockService.Object.UpdateTarifa(dto, idTarifa);

        // Assert
        Assert.False(validation.IsValid);
        Assert.False(resultado.Success);
        Assert.Equal(EResultType.BadRequest, resultado.ResultType);
        Assert.NotNull(resultado.Errors);
    }
}