using SuperProyecto.Core.Persistencia;
using SuperProyecto.Services.Validators;
using SuperProyecto.Core.Enums;

namespace SuperProyecto.Tests;

public class TestAdoCliente
{
    [Fact]
    public void CuandoObtengoLosClientes_DebeRetornarUnaListaDeClientes_ConResultadoOk()
    {
        // Arrange
        var mockServicio = new Mock<IClienteService>();
        var clientes = new List<ClienteResponse>
        {
            new ClienteResponse { DNI = 12345678, idUsuario = 1, nombre = "Juan", apellido = "Perez" },
            new ClienteResponse { DNI = 87654321, idUsuario = 2, nombre = "Ana", apellido = "Gomez" }
        };

        mockServicio.Setup(s => s.GetClientes())
            .Returns(Result<IEnumerable<ClienteResponse>>.Ok(clientes));

        // Act
        var resultado = mockServicio.Object.GetClientes();

        // Assert
        Assert.True(resultado.Success);
        Assert.Equal(EResultType.Ok, resultado.ResultType);
        Assert.Equal(clientes.Count, resultado.Data.Count());
    }

    [Fact]
    public void CuandoObtengoUnClienteInexistente_DebeRetornarNotFound()
    {
        // Arrange
        var mockServicio = new Mock<IClienteService>();
        int idCliente = 1;
        mockServicio.Setup(s => s.DetalleCliente(idCliente))
            .Returns(Result<ClienteResponse>.NotFound("No encontrado"));

        // Act
        var resultado = mockServicio.Object.DetalleCliente(idCliente);

        // Assert
        Assert.False(resultado.Success);
        Assert.Equal(EResultType.NotFound, resultado.ResultType);
        Assert.Null(resultado.Data);
    }

    [Fact]
    public void CuandoDoyDeAltaUnClienteValido_DebeCrearCliente_ConResultadoCreated()
    {
        // Arrange
        var mockRepoUsuario = new Mock<IRepoUsuario>();
        var mockRepoCliente = new Mock<IRepoCliente>();

        var dto = new ClienteDtoAlta
        {
            DNI = 12345678,
            idUsuario = 3,
            nombre = "Carlos",
            apellido = "Lopez"
        };

        mockRepoUsuario.Setup(r => r.DetalleUsuario(dto.idUsuario))
            .Returns(new Usuario { idUsuario = dto.idUsuario, rol = ERol.Cliente });

        mockRepoCliente.Setup(r => r.DetalleCliente(dto.DNI))
            .Returns((Cliente)null);

        var validator = new ClienteDtoAltaValidator(mockRepoUsuario.Object, mockRepoCliente.Object);
        var validationResult = validator.Validate(dto);

        var mockServicio = new Mock<IClienteService>();
        mockServicio.Setup(s => s.AltaCliente(dto))
            .Returns(Result<ClienteResponse>.Created(new ClienteResponse { DNI = dto.DNI, idUsuario = dto.idUsuario, nombre = dto.nombre, apellido = dto.apellido }));

        // Act
        var resultado = mockServicio.Object.AltaCliente(dto);

        // Assert
        Assert.True(resultado.Success);
        Assert.Equal(EResultType.Created, resultado.ResultType);
        Assert.Equal(dto.DNI, resultado.Data.DNI);
        Assert.Equal(dto.idUsuario, resultado.Data.idUsuario);
        Assert.Equal(dto.nombre, resultado.Data.nombre);
        Assert.Equal(dto.apellido, resultado.Data.apellido);
    }

    [Fact]
    public void CuandoDoyDeAltaUnClienteInvalido_DebeRetornarBadRequest()
    {
        // Arrange
        var mockRepoUsuario = new Mock<IRepoUsuario>();
        var mockRepoCliente = new Mock<IRepoCliente>();

        var dto = new ClienteDtoAlta
        {
            DNI = 0,
            idUsuario = -5,
            nombre = "Ca",
            apellido = ""
        };

        mockRepoUsuario.Setup(r => r.DetalleUsuario(dto.idUsuario))
            .Returns((Usuario)null);

        mockRepoCliente.Setup(r => r.DetalleCliente(dto.DNI))
            .Returns(new Cliente());

        var validator = new ClienteDtoAltaValidator(mockRepoUsuario.Object, mockRepoCliente.Object);
        var validationResult = validator.Validate(dto);

        var mockServicio = new Mock<IClienteService>();
        mockServicio.Setup(s => s.AltaCliente(dto))
            .Returns(Result<ClienteResponse>.BadRequest(validationResult.ToDictionary()));

        // Act
        var resultado = mockServicio.Object.AltaCliente(dto);

        // Assert
        Assert.False(validationResult.IsValid);
        Assert.False(resultado.Success);
        Assert.Equal(EResultType.BadRequest, resultado.ResultType);
        Assert.NotNull(resultado.Errors);
    }

    [Fact]
    public void CuandoActualizoUnClienteValido_DebeRetornarOk()
    {
        // Arrange
        var dto = new ClienteDtoUpdate
        {
            nombre = "Luis",
            apellido = "Martinez"
        };

        var validator = new ClienteDtoUpdateValidator();
        var validationResult = validator.Validate(dto);

        int idCliente = 10;

        var mockServicio = new Mock<IClienteService>();
        mockServicio.Setup(s => s.UpdateCliente(dto, idCliente))
            .Returns(Result<ClienteResponse>.Ok(new ClienteResponse { nombre = dto.nombre, apellido = dto.apellido }));

        // Act
        var resultado = mockServicio.Object.UpdateCliente(dto, idCliente);

        // Assert
        Assert.True(validationResult.IsValid);
        Assert.True(resultado.Success);
        Assert.Equal(EResultType.Ok, resultado.ResultType);
        Assert.Equal(dto.nombre, resultado.Data.nombre);
        Assert.Equal(dto.apellido, resultado.Data.apellido);
    }

    [Fact]
    public void CuandoActualizoUnClienteInvalido_DebeRetornarBadRequest()
    {
        // Arrange
        var dto = new ClienteDtoUpdate
        {
            nombre = "Lu",
            apellido = ""
        };

        var validator = new ClienteDtoUpdateValidator();
        var validationResult = validator.Validate(dto);

        int idCliente = 2;

        var mockServicio = new Mock<IClienteService>();
        mockServicio.Setup(s => s.UpdateCliente(dto, idCliente))
            .Returns(Result<ClienteResponse>.BadRequest(validationResult.ToDictionary()));

        // Act
        var resultado = mockServicio.Object.UpdateCliente(dto, idCliente);

        // Assert
        Assert.False(validationResult.IsValid);
        Assert.False(resultado.Success);
        Assert.Equal(EResultType.BadRequest, resultado.ResultType);
        Assert.NotNull(resultado.Errors);
    }
}
