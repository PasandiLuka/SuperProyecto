using SuperProyecto.Core.Enums;
using SuperProyecto.Core.Persistencia;
using SuperProyecto.Services.Validators;

namespace SuperProyecto.Tests;

public class TestAdoOrden
{
    [Fact]
    public void CuandoObtengoLasOrdenes_DebeRetornarUnaListaDeOrdenes_ConResultadoOk()
    {
        // Arrange
        var mockService = new Mock<IOrdenService>();
        var ordenes = new List<Orden>
        {
            new Orden { idOrden = 1, idCliente = 12345678, fecha = DateTime.Today, pagada = false },
            new Orden { idOrden = 2, idCliente = 87654321, fecha = DateTime.Today, pagada = true }
        };

        mockService.Setup(s => s.GetOrdenes()).Returns(Result<IEnumerable<Orden>>.Ok(ordenes));

        // Act
        var resultado = mockService.Object.GetOrdenes();

        // Assert
        Assert.True(resultado.Success);
        Assert.Equal(EResultType.Ok, resultado.ResultType);
        Assert.Equal(ordenes.Count, resultado.Data.Count());
    }

    [Fact]
    public void CuandoBuscoDetalleDeUnaOrdenValida_DebeRetornarOrden_ConResultadoOk()
    {
        // Arrange
        var mockService = new Mock<IOrdenService>();
        var orden = new Orden { idOrden = 1, idCliente = 12345678, fecha = DateTime.Today, pagada = false };

        mockService.Setup(s => s.DetalleOrden(orden.idOrden)).Returns(Result<Orden>.Ok(orden));

        // Act
        var resultado = mockService.Object.DetalleOrden(orden.idOrden);

        // Assert
        Assert.True(resultado.Success);
        Assert.Equal(orden.idOrden, resultado.Data.idOrden);
        Assert.Equal(orden.idCliente, resultado.Data.idCliente);
    }

    [Fact]
    public void CuandoRealizoUnAltaDeOrdenValida_DebeRetornarCreated()
    {
        // Arrange
        var mockRepoCliente = new Mock<IRepoCliente>();
        var validator = new OrdenValidator(mockRepoCliente.Object);

        mockRepoCliente.Setup(r => r.DetalleCliente(It.IsAny<int>())).Returns(new Cliente { DNI = 12345678 });

        var orden = new OrdenDto { idCliente = 12345678};

        // Act
        var validationResult = validator.Validate(orden);
        Result<Orden> resultado;
        if (!validationResult.IsValid)
        {
            var errores = validationResult.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());
            resultado = Result<Orden>.BadRequest(errores);
        }
        else
        {
            resultado = Result<Orden>.Created(new Orden
            {
                idOrden = 1,
                idCliente = orden.idCliente,
                fecha = DateTime.Today,
                pagada = false
            });
        }

        // Assert
        Assert.True(resultado.Success);
        Assert.Equal(EResultType.Created, resultado.ResultType);
        Assert.Equal(orden.idCliente, resultado.Data.idCliente);
    }

    [Fact]
    public void CuandoRealizoUnAltaDeOrdenInvalida_DebeRetornarBadRequest()
    {
        // Arrange
        var mockRepoCliente = new Mock<IRepoCliente>();
        var validator = new OrdenValidator(mockRepoCliente.Object);

        // Simulamos cliente inexistente y función cancelada
        mockRepoCliente.Setup(r => r.DetalleCliente(It.IsAny<int>())).Returns((Cliente)null);

        var orden = new OrdenDto { idCliente = 0};

        // Act
        var validationResult = validator.Validate(orden);
        Result<Orden> resultado;
        if (!validationResult.IsValid)
        {
            var errores = validationResult.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());
            resultado = Result<Orden>.BadRequest(errores);
        }
        else
        {
            resultado = Result<Orden>.Created(new Orden
            {
                idOrden = 1,
                idCliente = orden.idCliente,
                fecha = DateTime.Today,
                pagada = false
            });
        }

        // Assert
        Assert.False(resultado.Success);
        Assert.Equal(EResultType.BadRequest, resultado.ResultType);
        Assert.True(resultado.Errors.ContainsKey("idCliente"));
    }

    [Fact]
    public void CuandoActualizoUnaOrdenValida_DebeRetornarOk()
    {
        // Arrange
        var mockRepoCliente = new Mock<IRepoCliente>();
        var mockRepoSector = new Mock<IRepoSector>();
        var validator = new OrdenValidator(mockRepoCliente.Object);

        mockRepoCliente.Setup(r => r.DetalleCliente(It.IsAny<int>())).Returns(new Cliente { DNI = 12345678 });
        mockRepoSector.Setup(r => r.DetalleSector(It.IsAny<int>())).Returns(new Sector { idSector = 1, idLocal = 1, nombre = "nombre"});

        var orden = new OrdenDto { idCliente = 12345678};

        // Act
        var validationResult = validator.Validate(orden);
        Result<Orden> resultado;
        if (!validationResult.IsValid)
        {
            var errores = validationResult.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());
            resultado = Result<Orden>.BadRequest(errores);
        }
        else
        {
            resultado = Result<Orden>.Ok(new Orden
            {
                idOrden = 1,
                idCliente = orden.idCliente,
                fecha = DateTime.Today,
                pagada = false
            });
        }

        // Assert
        Assert.True(resultado.Success);
        Assert.Equal(EResultType.Ok, resultado.ResultType);
        Assert.Equal(orden.idCliente, resultado.Data.idCliente);
    }

    [Fact]
    public void CuandoActualizoUnaOrdenInvalida_DebeRetornarBadRequest()
    {
        // Arrange
        var mockRepoCliente = new Mock<IRepoCliente>();
        var validator = new OrdenValidator(mockRepoCliente.Object);

        mockRepoCliente.Setup(r => r.DetalleCliente(It.IsAny<int>())).Returns((Cliente)null);

        var orden = new OrdenDto { idCliente = 0};

        // Act
        var validationResult = validator.Validate(orden);
        Result<Orden> resultado;
        if (!validationResult.IsValid)
        {
            var errores = validationResult.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());
            resultado = Result<Orden>.BadRequest(errores);
        }
        else
        {
            resultado = Result<Orden>.Ok(new Orden
            {
                idOrden = 1,
                idCliente = orden.idCliente,
                fecha = DateTime.Today,
                pagada = false
            });
        }

        // Assert
        Assert.False(resultado.Success);
        Assert.Equal(EResultType.BadRequest, resultado.ResultType);
        Assert.True(resultado.Errors.ContainsKey("idCliente"));
    }
}