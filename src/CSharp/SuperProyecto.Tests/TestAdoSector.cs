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
        var sectores = new List<Sector>
        {
            new Sector { idSector = 1, idLocal = 1, nombre = "Sector A" },
            new Sector { idSector = 2, idLocal = 2, nombre = "Sector B" }
        };

        mockService.Setup(s => s.GetSectores())
            .Returns(Result<IEnumerable<Sector>>.Ok(sectores));

        // Act
        var resultado = mockService.Object.GetSectores();

        // Assert
        Assert.True(resultado.Success);
        Assert.Equal(EResultType.Ok, resultado.ResultType);
        Assert.Equal(sectores.Count, resultado.Data.Count());
    }

    [Fact]
    public void CuandoBuscoDetalleDeUnSectorValido_DebeRetornarSector_ConResultadoOk()
    {
        // Arrange
        var mockService = new Mock<ISectorService>();
        var sector = new Sector { idSector = 1, idLocal = 1, nombre = "Sector A" };

        mockService.Setup(s => s.DetalleSector(sector.idSector))
            .Returns(Result<Sector>.Ok(sector));

        // Act
        var resultado = mockService.Object.DetalleSector(sector.idSector);

        // Assert
        Assert.True(resultado.Success);
        Assert.Equal(sector.idSector, resultado.Data.idSector);
        Assert.Equal(sector.nombre, resultado.Data.nombre);
    }

    [Fact]
    public void CuandoRealizoUnAltaDeSectorValido_DebeRetornarCreated()
    {
        // Arrange
        var mockService = new Mock<ISectorService>();
        var sectorDto = new SectorDto { idLocal = 1, nombre = "Sector C" };

        mockService.Setup(s => s.AltaSector(sectorDto))
            .Returns(Result<SectorDto>.Created(new SectorDto
            {
                idLocal = sectorDto.idLocal,
                nombre = sectorDto.nombre
            }));

        // Act
        var resultado = mockService.Object.AltaSector(sectorDto);

        // Assert
        Assert.True(resultado.Success);
        Assert.Equal(EResultType.Created, resultado.ResultType);
        Assert.Equal(sectorDto.idLocal, resultado.Data.idLocal);
        Assert.Equal(sectorDto.nombre, resultado.Data.nombre);
    }

    [Fact]
    public void CuandoActualizoUnSectorValido_DebeRetornarOk()
    {
        // Arrange
        var mockService = new Mock<ISectorService>();
        var sectorDto = new SectorDto { idLocal = 2, nombre = "Sector D" };
        int idSector = 1;

        mockService.Setup(s => s.UpdateSector(sectorDto, idSector))
            .Returns(Result<SectorDto>.Ok(new SectorDto
            {
                idLocal = sectorDto.idLocal,
                nombre = sectorDto.nombre
            }));

        // Act
        var resultado = mockService.Object.UpdateSector(sectorDto, idSector);

        // Assert
        Assert.True(resultado.Success);
        Assert.Equal(EResultType.Ok, resultado.ResultType);
        Assert.Equal(sectorDto.nombre, resultado.Data.nombre);
    }

    [Fact]
    public void CuandoRealizoUnAltaDeSectorInvalido_DebeRetornarBadRequest()
    {
        // Arrange
        var mockRepoLocal = new Mock<IRepoLocal>();
        var mockRepoFuncion = new Mock<IRepoFuncion>();
        var mockRepoTarifa = new Mock<IRepoTarifa>();
        mockRepoLocal.Setup(r => r.DetalleLocal(It.IsAny<int>())).Returns((Local?)null);

        var sectorDto = new SectorDto { idLocal = 0, nombre = "AB" };
        var validator = new SectorValidator(mockRepoLocal.Object, mockRepoFuncion.Object, mockRepoTarifa.Object);

        // Act
        var validationResult = validator.Validate(sectorDto);

        Result<Sector> resultado;
        if (!validationResult.IsValid)
        {
            var errores = validationResult.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

            resultado = Result<Sector>.BadRequest(errores);
        }
        else
        {
            resultado = Result<Sector>.Created(new Sector
            {
                idLocal = sectorDto.idLocal,
                nombre = sectorDto.nombre
            });
        }

        // Assert
        Assert.False(resultado.Success);
        Assert.Equal(EResultType.BadRequest, resultado.ResultType);
        Assert.True(resultado.Errors.ContainsKey("idLocal"));
        Assert.True(resultado.Errors.ContainsKey("nombre"));
    }
}