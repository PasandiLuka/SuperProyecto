using SuperProyecto.Core.Enums;
using SuperProyecto.Core.Persistencia;
using SuperProyecto.Services.Validators;

namespace SuperProyecto.Tests;

public class TestAdoUsuario
{
    [Fact]
    public void CuandoObtengoLosRoles_DebeRetornarListaDeRoles_ConResultadoOk()
    {
        // Arrange
        var mockService = new Mock<IUsuarioService>();
        var roles = Enum.GetNames(typeof(ERol));

        mockService.Setup(s => s.ObtenerRoles())
            .Returns(Result<string[]>.Ok(roles));

        // Act
        var resultado = mockService.Object.ObtenerRoles();

        // Assert
        Assert.True(resultado.Success);
        Assert.Equal(EResultType.Ok, resultado.ResultType);
        Assert.Equal(roles.Length, resultado.Data.Length);
    }

    [Fact]
    public void CuandoBuscoDetalleDeUsuarioValido_DebeRetornarUsuario_ConResultadoOk()
    {
        // Arrange
        var mockService = new Mock<IUsuarioService>();
        var usuario = new Usuario { idUsuario = 1, email = "usuario@test.com", passwordHash = "123456", rol = ERol.Administrador };

        mockService.Setup(s => s.DetalleUsuario(usuario.idUsuario))
            .Returns(Result<Usuario?>.Ok(usuario));

        // Act
        var resultado = mockService.Object.DetalleUsuario(usuario.idUsuario);

        // Assert
        Assert.True(resultado.Success);
        Assert.Equal(EResultType.Ok, resultado.ResultType);
        Assert.Equal(usuario.idUsuario, resultado.Data.idUsuario);
        Assert.Equal(usuario.email, resultado.Data.email);
    }

    [Fact]
    public void CuandoBuscoDetalleUsuarioPorEmailValido_DebeRetornarUsuario_ConResultadoOk()
    {
        // Arrange
        var mockService = new Mock<IUsuarioService>();
        var usuario = new Usuario { idUsuario = 2, email = "correo@test.com", passwordHash = "abcdef", rol = ERol.Organizador };

        mockService.Setup(s => s.DetalleUsuarioXEmail(usuario.email))
            .Returns(Result<Usuario?>.Ok(usuario));

        // Act
        var resultado = mockService.Object.DetalleUsuarioXEmail(usuario.email);

        // Assert
        Assert.True(resultado.Success);
        Assert.Equal(EResultType.Ok, resultado.ResultType);
        Assert.Equal(usuario.email, resultado.Data.email);
        Assert.Equal(usuario.rol, resultado.Data.rol);
    }

    [Fact]
    public void CuandoActualizoElRolDeUnUsuario_DebeRetornarOk()
    {
        // Arrange
        var mockService = new Mock<IUsuarioService>();
        var usuario = new Usuario { idUsuario = 3, email = "rol@test.com", passwordHash = "pass123", rol = ERol.Organizador };
        var nuevoRol = ERol.Administrador;

        mockService.Setup(s => s.ActualizarRol(usuario.idUsuario, nuevoRol))
            .Returns(Result<Usuario>.Ok());

        // Act
        var resultado = mockService.Object.ActualizarRol(usuario.idUsuario, nuevoRol);

        // Assert
        Assert.True(resultado.Success);
        Assert.Equal(EResultType.Ok, resultado.ResultType);
    }

    [Fact]
    public void CuandoRealizoUnAltaDeUsuarioValido_DebeRetornarCreated()
    {
        // Arrange
        var mockService = new Mock<IUsuarioService>();
        var usuarioDto = new UsuarioDto { email = "nuevo@correo.com", password = "segura123", Rol = ERol.Organizador };

        mockService.Setup(s => s.AltaUsuario(usuarioDto))
            .Returns(Result<Usuario>.Created(new Usuario
            {
                email = usuarioDto.email,
                passwordHash = usuarioDto.password,
                rol = usuarioDto.Rol
            }));

        // Act
        var resultado = mockService.Object.AltaUsuario(usuarioDto);

        // Assert
        Assert.True(resultado.Success);
        Assert.Equal(EResultType.Created, resultado.ResultType);
        Assert.Equal(usuarioDto.email, resultado.Data.email);
        Assert.Equal(usuarioDto.Rol, resultado.Data.rol);
    }

    [Fact]
    public void CuandoRealizoUnAltaDeUsuarioInvalido_DebeRetornarBadRequest()
    {
        // Arrange
        var mockRepoUsuario = new Mock<IRepoUsuario>();
        mockRepoUsuario.Setup(r => r.UniqueEmail(It.IsAny<string>())).Returns(false);

        var usuarioDto = new UsuarioDto
        {
            email = "correoInvalido",
            password = "123",
            Rol = (ERol)999
        };

        var validator = new UsuarioValidator(mockRepoUsuario.Object);

        // Act
        var validationResult = validator.Validate(usuarioDto);

        Result<Usuario> resultado;
        if (!validationResult.IsValid)
        {
            var errores = validationResult.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

            resultado = Result<Usuario>.BadRequest(errores);
        }
        else
        {
            resultado = Result<Usuario>.Created(new Usuario
            {
                email = usuarioDto.email,
                passwordHash = usuarioDto.password,
                rol = usuarioDto.Rol
            });
        }

        // Assert
        Assert.False(resultado.Success);
        Assert.Equal(EResultType.BadRequest, resultado.ResultType);
        Assert.True(resultado.Errors.ContainsKey("email"));
        Assert.True(resultado.Errors.ContainsKey("password"));
        Assert.True(resultado.Errors.ContainsKey("Rol"));
    }
}