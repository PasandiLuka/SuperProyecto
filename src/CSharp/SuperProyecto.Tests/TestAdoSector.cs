using SuperProyecto.Core.Enums;
using SuperProyecto.Core.Persistencia;
using SuperProyecto.Services.Validators;

namespace SuperProyecto.Tests;

public class TestAdoSector
{
    [Fact]
    public void CuandoObtengoLosSectores_DebeRetornarUnaListaDeSectores_ConResultadoOk()
    {
        // Arrange
        var mockService = new Mock<ISectorService>();
        int idLocal = 1;

        var sectores = new List<Sector>
        {
            new Sector { idSector = 1, idLocal = idLocal, nombre = "Platea", eliminado = false },
            new Sector { idSector = 2, idLocal = idLocal, nombre = "VIP", eliminado = false }
        };

        mockService.Setup(s => s.GetSectores(idLocal))
            .Returns(Result<IEnumerable<Sector>>.Ok(sectores));

        // Act
        var resultado = mockService.Object.GetSectores(idLocal);

        // Assert
        Assert.True(resultado.Success);
        Assert.Equal(EResultType.Ok, resultado.ResultType);
        Assert.Equal(sectores.Count, resultado.Data.Count());
    }

    [Fact]
    public void CuandoObtengoLosSectoresDeUnLocalSinSectores_DebeRetornarListaVacia()
    {
        // Arrange
        var mockService = new Mock<ISectorService>();
        int idLocal = 5;

        var sectores = new List<Sector>();

        mockService.Setup(s => s.GetSectores(idLocal))
            .Returns(Result<IEnumerable<Sector>>.Ok(sectores));

        // Act
        var resultado = mockService.Object.GetSectores(idLocal);

        // Assert
        Assert.True(resultado.Success);
        Assert.Equal(EResultType.Ok, resultado.ResultType);
        Assert.Equal(sectores.Count, resultado.Data.Count());
    }

    [Fact]
    public void CuandoObtengoDetalleDeSectorValido_DebeRetornarSector_ConResultadoOk()
    {
        // Arrange
        var mockService = new Mock<ISectorService>();
        int idSector = 10;

        var sector = new Sector
        {
            idSector = idSector,
            idLocal = 1,
            nombre = "Popular",
            eliminado = false
        };

        mockService.Setup(s => s.DetalleSector(idSector))
            .Returns(Result<Sector?>.Ok(sector));

        // Act
        var resultado = mockService.Object.DetalleSector(idSector);

        // Assert
        Assert.True(resultado.Success);
        Assert.Equal(EResultType.Ok, resultado.ResultType);
        Assert.Equal(sector.idSector, resultado.Data.idSector);
        Assert.Equal(sector.nombre, resultado.Data.nombre);
        Assert.Equal(sector.eliminado, resultado.Data.eliminado);
    }

    [Fact]
    public void CuandoObtengoDetalleDeSectorInexistente_DebeRetornarNotFound()
    {
        // Arrange
        var mockService = new Mock<ISectorService>();
        int idSector = 99;

        mockService.Setup(s => s.DetalleSector(idSector))
            .Returns(Result<Sector?>.NotFound("Sector no encontrado"));

        // Act
        var resultado = mockService.Object.DetalleSector(idSector);

        // Assert
        Assert.False(resultado.Success);
        Assert.Equal(EResultType.NotFound, resultado.ResultType);
        Assert.Null(resultado.Data);
    }

    [Fact]
    public void CuandoDoyDeAltaSectorValido_DebeRetornarCreated()
    {
        // Arrange
        var mockRepoLocal = new Mock<IRepoLocal>();

        var dto = new SectorDto
        {
            nombre = "Palco",
            idLocal = 1,
            eliminado = false
        };

        mockRepoLocal.Setup(r => r.DetalleLocal(dto.idLocal))
            .Returns(new Local { idLocal = dto.idLocal, nombre = "Teatro" });

        var validator = new SectorValidator(mockRepoLocal.Object);
        var validation = validator.Validate(dto);

        var mockService = new Mock<ISectorService>();
        mockService.Setup(s => s.AltaSector(dto, dto.idLocal))
            .Returns(Result<SectorDto>.Created(dto));

        // Act
        var resultado = mockService.Object.AltaSector(dto, dto.idLocal);

        // Assert
        Assert.True(validation.IsValid);
        Assert.True(resultado.Success);
        Assert.Equal(EResultType.Created, resultado.ResultType);
        Assert.Equal(dto.nombre, resultado.Data.nombre);
        Assert.Equal(dto.eliminado, resultado.Data.eliminado);
    }

    [Fact]
    public void CuandoDoyDeAltaSectorInvalido_DebeRetornarBadRequest()
    {
        // Arrange
        var mockRepoLocal = new Mock<IRepoLocal>();

        var dto = new SectorDto
        {
            nombre = "A", // muy corto
            eliminado = false
        };

        int idLocal = 1;

        mockRepoLocal.Setup(r => r.DetalleLocal(idLocal))
            .Returns(new Local { idLocal = idLocal, nombre = "Estadio" });

        var validator = new SectorValidator(mockRepoLocal.Object);
        var validation = validator.Validate(dto);

        var mockService = new Mock<ISectorService>();
        mockService.Setup(s => s.AltaSector(dto, idLocal))
            .Returns(Result<SectorDto>.BadRequest(validation.ToDictionary()));

        // Act
        var resultado = mockService.Object.AltaSector(dto, idLocal);

        // Assert
        Assert.False(validation.IsValid);
        Assert.False(resultado.Success);
        Assert.Equal(EResultType.BadRequest, resultado.ResultType);
        Assert.NotNull(resultado.Errors);
    }

    [Fact]
    public void CuandoActualizoSectorValido_DebeRetornarOk()
    {
        // Arrange
        var mockRepoLocal = new Mock<IRepoLocal>();

        var dto = new SectorDto
        {
            nombre = "Popular Nueva",
            idLocal = 1,
            eliminado = false
        };

        int idSector = 10;

        mockRepoLocal.Setup(r => r.DetalleLocal(It.IsAny<int>()))
            .Returns(new Local());

        var validator = new SectorValidator(mockRepoLocal.Object);
        var validation = validator.Validate(dto);

        var mockService = new Mock<ISectorService>();
        mockService.Setup(s => s.UpdateSector(dto, idSector))
            .Returns(Result<SectorDto>.Ok(dto));

        // Act
        var resultado = mockService.Object.UpdateSector(dto, idSector);

        // Assert
        Assert.True(validation.IsValid);
        Assert.True(resultado.Success);
        Assert.Equal(EResultType.Ok, resultado.ResultType);
        Assert.Equal(dto.nombre, resultado.Data.nombre);
        Assert.Equal(dto.eliminado, resultado.Data.eliminado);
    }

    [Fact]
    public void CuandoActualizoSectorInvalido_DebeRetornarBadRequest()
    {
        // Arrange
        var mockRepoLocal = new Mock<IRepoLocal>();

        var dto = new SectorDto
        {
            nombre = "", // inválido
            eliminado = false
        };

        int idSector = 4;

        mockRepoLocal.Setup(r => r.DetalleLocal(It.IsAny<int>()))
            .Returns(new Local());

        var validator = new SectorValidator(mockRepoLocal.Object);
        var validation = validator.Validate(dto);

        var mockService = new Mock<ISectorService>();
        mockService.Setup(s => s.UpdateSector(dto, idSector))
            .Returns(Result<SectorDto>.BadRequest(validation.ToDictionary()));

        // Act
        var resultado = mockService.Object.UpdateSector(dto, idSector);

        // Assert
        Assert.False(validation.IsValid);
        Assert.False(resultado.Success);
        Assert.Equal(EResultType.BadRequest, resultado.ResultType);
        Assert.NotNull(resultado.Errors);
    }

    [Fact]
    public void CuandoEliminoSectorValido_DebeRetornarOk()
    {
        // Arrange
        var mockService = new Mock<ISectorService>();
        int idSector = 3;

        var dto = new SectorDto { nombre = "Popular", eliminado = true };

        mockService.Setup(s => s.DeleteSector(idSector))
            .Returns(Result<SectorDto>.Ok(dto));

        // Act
        var resultado = mockService.Object.DeleteSector(idSector);

        // Assert
        Assert.True(resultado.Success);
        Assert.Equal(EResultType.Ok, resultado.ResultType);
        Assert.Equal(dto.eliminado, resultado.Data.eliminado);
        Assert.Equal(dto.nombre, resultado.Data.nombre);
    }
}
