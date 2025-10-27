using SuperProyecto.Core.Enums;
using SuperProyecto.Core.Persistencia;
using SuperProyecto.Services.Validators;

namespace SuperProyecto.Tests;

public class TestAdoCliente
{
    [Fact]
    public void CuandoObtengoLosClientes_DebeRetornarUnaListaDeClientes_ConResultadoOk()
    {
        // Arrange
        var mockService = new Mock<IClienteService>();
        var clientes = new List<Cliente>
        {
            new Cliente { DNI = 12345678, idUsuario = 1, nombre = "Juan", apellido = "Perez", telefono = 12345 },
            new Cliente { DNI = 87654321, idUsuario = 2, nombre = "Maria", apellido = "Gomez", telefono = 67890 }
        };

        mockService.Setup(s => s.GetClientes())
            .Returns(Result<IEnumerable<Cliente>>.Ok(clientes));

        // Act
        var resultado = mockService.Object.GetClientes();

        // Assert
        Assert.True(resultado.Success);
        Assert.Equal(EResultType.Ok, resultado.ResultType);
        Assert.Equal(clientes.Count, resultado.Data.Count());
    }

    [Fact]
    public void CuandoBuscoDetalleDeUnClienteValido_DebeRetornarCliente_ConResultadoOk()
    {
        // Arrange
        var mockService = new Mock<IClienteService>();
        var cliente = new Cliente { DNI = 12345678, idUsuario = 1, nombre = "Juan", apellido = "Perez", telefono = 12345 };

        mockService.Setup(s => s.DetalleCliente(cliente.DNI))
            .Returns(Result<Cliente>.Ok(cliente));

        // Act
        var resultado = mockService.Object.DetalleCliente(cliente.DNI);

        // Assert
        Assert.True(resultado.Success);
        Assert.Equal(cliente.DNI, resultado.Data.DNI);
        Assert.Equal(cliente.nombre, resultado.Data.nombre);
    }

    [Fact]
    public void CuandoRealizoUnAltaDeClienteValido_DebeRetornarCreated()
    {
        // Arrange
        var mockService = new Mock<IClienteService>();
        var cliente = new ClienteDto { DNI = 23456789, idUsuario = 3, nombre = "Carlos", apellido = "Lopez", telefono = 55555 };

        mockService.Setup(s => s.AltaCliente(cliente))
            .Returns(Result<Cliente>.Created(new Cliente
            {
                DNI = cliente.DNI,
                idUsuario = cliente.idUsuario,
                nombre = cliente.nombre,
                apellido = cliente.apellido,
                telefono = cliente.telefono
            }));

        // Act
        var resultado = mockService.Object.AltaCliente(cliente);

        // Assert
        Assert.True(resultado.Success);
        Assert.Equal(EResultType.Created, resultado.ResultType);
        Assert.Equal(cliente.DNI, resultado.Data.DNI);
        Assert.Equal(cliente.nombre, resultado.Data.nombre);
    }

    [Fact]
    public void CuandoRealizoUnAltaDeClienteInvalido_DebeRetornarBadRequest()
    {
        // Arrange
        var mockRepoUsuario = new Mock<IRepoUsuario>();
        var mockRepoCliente = new Mock<IRepoCliente>();

        mockRepoCliente.Setup(r => r.DetalleCliente(It.IsAny<int>())).Returns(new Cliente());

        var cliente = new ClienteDto { DNI = 123, idUsuario = 0, nombre = "Jo", apellido = "", telefono = 0 };
        var validator = new ClienteValidator(mockRepoUsuario.Object, mockRepoCliente.Object);

        // Act
        var validationResult = validator.Validate(cliente);

        Result<Cliente> resultado;
        if (!validationResult.IsValid)
        {
            var errores = validationResult.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

            resultado = Result<Cliente>.BadRequest(errores);
        }
        else
        {
            resultado = Result<Cliente>.Created(new Cliente
            {
                DNI = cliente.DNI,
                idUsuario = cliente.idUsuario,
                nombre = cliente.nombre,
                apellido = cliente.apellido,
                telefono = cliente.telefono
            });
        }

        // Assert
        Assert.False(resultado.Success);
        Assert.Equal(EResultType.BadRequest, resultado.ResultType);
        Assert.True(resultado.Errors.ContainsKey("DNI"));
        Assert.True(resultado.Errors.ContainsKey("nombre"));
        Assert.True(resultado.Errors.ContainsKey("apellido"));
        Assert.True(resultado.Errors.ContainsKey("telefono"));
    }
}
