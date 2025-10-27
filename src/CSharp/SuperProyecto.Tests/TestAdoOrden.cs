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
            new Orden { idOrden = 1, DNI = 12345678, idFuncion = 1, fecha = DateTime.Today, pagada = false },
            new Orden { idOrden = 2, DNI = 87654321, idFuncion = 2, fecha = DateTime.Today, pagada = true }
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
        var orden = new Orden { idOrden = 1, DNI = 12345678, idFuncion = 1, fecha = DateTime.Today, pagada = false };

        mockService.Setup(s => s.DetalleOrden(orden.idOrden)).Returns(Result<Orden>.Ok(orden));

        // Act
        var resultado = mockService.Object.DetalleOrden(orden.idOrden);

        // Assert
        Assert.True(resultado.Success);
        Assert.Equal(orden.idOrden, resultado.Data.idOrden);
        Assert.Equal(orden.DNI, resultado.Data.DNI);
        Assert.Equal(orden.idFuncion, resultado.Data.idFuncion);
    }

    [Fact]
    public void CuandoRealizoUnAltaDeOrdenValida_DebeRetornarCreated()
    {
        // Arrange
        var mockRepoCliente = new Mock<IRepoCliente>();
        var mockRepoFuncion = new Mock<IRepoFuncion>();
        var validator = new OrdenValidator(mockRepoCliente.Object, mockRepoFuncion.Object);

        mockRepoCliente.Setup(r => r.DetalleCliente(It.IsAny<int>())).Returns(new Cliente { DNI = 12345678 });
        mockRepoFuncion.Setup(r => r.DetalleFuncion(It.IsAny<int>())).Returns(new Funcion { idFuncion = 1, cancelada = false, stock = 10 });

        var orden = new OrdenDto { DNI = 12345678, idFuncion = 1 };

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
                DNI = orden.DNI,
                idFuncion = orden.idFuncion,
                fecha = DateTime.Today,
                pagada = false
            });
        }

        // Assert
        Assert.True(resultado.Success);
        Assert.Equal(EResultType.Created, resultado.ResultType);
        Assert.Equal(orden.DNI, resultado.Data.DNI);
        Assert.Equal(orden.idFuncion, resultado.Data.idFuncion);
    }

    [Fact]
    public void CuandoRealizoUnAltaDeOrdenInvalida_DebeRetornarBadRequest()
    {
        // Arrange
        var mockRepoCliente = new Mock<IRepoCliente>();
        var mockRepoFuncion = new Mock<IRepoFuncion>();
        var validator = new OrdenValidator(mockRepoCliente.Object, mockRepoFuncion.Object);

        // Simulamos cliente inexistente y función cancelada
        mockRepoCliente.Setup(r => r.DetalleCliente(It.IsAny<int>())).Returns((Cliente)null);
        mockRepoFuncion.Setup(r => r.DetalleFuncion(It.IsAny<int>())).Returns(new Funcion { cancelada = true, stock = 0 });

        var orden = new OrdenDto { DNI = 0, idFuncion = 0 };

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
                DNI = orden.DNI,
                idFuncion = orden.idFuncion,
                fecha = DateTime.Today,
                pagada = false
            });
        }

        // Assert
        Assert.False(resultado.Success);
        Assert.Equal(EResultType.BadRequest, resultado.ResultType);
        Assert.True(resultado.Errors.ContainsKey("DNI"));
        Assert.True(resultado.Errors.ContainsKey("idFuncion"));
    }

    [Fact]
    public void CuandoActualizoUnaOrdenValida_DebeRetornarOk()
    {
        // Arrange
        var mockRepoCliente = new Mock<IRepoCliente>();
        var mockRepoFuncion = new Mock<IRepoFuncion>();
        var validator = new OrdenValidator(mockRepoCliente.Object, mockRepoFuncion.Object);

        mockRepoCliente.Setup(r => r.DetalleCliente(It.IsAny<int>())).Returns(new Cliente { DNI = 12345678 });
        mockRepoFuncion.Setup(r => r.DetalleFuncion(It.IsAny<int>())).Returns(new Funcion { idFuncion = 1, cancelada = false, stock = 10 });

        var orden = new OrdenDto { DNI = 12345678, idFuncion = 1 };

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
                DNI = orden.DNI,
                idFuncion = orden.idFuncion,
                fecha = DateTime.Today,
                pagada = false
            });
        }

        // Assert
        Assert.True(resultado.Success);
        Assert.Equal(EResultType.Ok, resultado.ResultType);
        Assert.Equal(orden.DNI, resultado.Data.DNI);
        Assert.Equal(orden.idFuncion, resultado.Data.idFuncion);
    }

    [Fact]
    public void CuandoActualizoUnaOrdenInvalida_DebeRetornarBadRequest()
    {
        // Arrange
        var mockRepoCliente = new Mock<IRepoCliente>();
        var mockRepoFuncion = new Mock<IRepoFuncion>();
        var validator = new OrdenValidator(mockRepoCliente.Object, mockRepoFuncion.Object);

        mockRepoCliente.Setup(r => r.DetalleCliente(It.IsAny<int>())).Returns((Cliente)null);
        mockRepoFuncion.Setup(r => r.DetalleFuncion(It.IsAny<int>())).Returns(new Funcion { cancelada = true, stock = 0 });

        var orden = new OrdenDto { DNI = 0, idFuncion = 0 };

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
                DNI = orden.DNI,
                idFuncion = orden.idFuncion,
                fecha = DateTime.Today,
                pagada = false
            });
        }

        // Assert
        Assert.False(resultado.Success);
        Assert.Equal(EResultType.BadRequest, resultado.ResultType);
        Assert.True(resultado.Errors.ContainsKey("DNI"));
        Assert.True(resultado.Errors.ContainsKey("idFuncion"));
    }
}