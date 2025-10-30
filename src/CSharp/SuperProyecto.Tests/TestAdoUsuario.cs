using SuperProyecto.Core.Enums;
using SuperProyecto.Core.Persistencia;
using SuperProyecto.Services.Validators;

namespace SuperProyecto.Tests;

public class TestAdoUsuario
{
    [Fact]
    public void CuandoObtengoLosRoles_DebeRetornarListaDeRoles_ConResultadoOk()
    {
        
        var mockService = new Mock<IUsuarioService>();
        var roles = Enum.GetNames(typeof(ERol));

        mockService.Setup(s => s.ObtenerRoles())
            .Returns(Result<string[]>.Ok(roles));

        
        var resultado = mockService.Object.ObtenerRoles();

        
        Assert.True(resultado.Success);
        Assert.Equal(EResultType.Ok, resultado.ResultType);
        Assert.Equal(roles.Length, resultado.Data.Length);
    }

    [Fact]
    public void CuandoBuscoDetalleDeUsuarioValido_DebeRetornarUsuario_ConResultadoOk()
    {
        
        var mockService = new Mock<IUsuarioService>();
        var usuario = new UsuarioDto {email = "usuario@test.com", password = "123456", Rol = ERol.Administrador };

        mockService.Setup(s => s.(usuario.idUsuario))
            .Returns(Result<UsuarioDto?>.Ok(usuario));

        
        var resultado = mockService.Object.DetalleUsuario(usuario.idUsuario);


        Assert.True(resultado.Success);
        Assert.Equal(EResultType.Ok, resultado.ResultType);
        Assert.Equal(usuario.idUsuario, resultado.Data.idUsuario);
        Assert.Equal(usuario.email, resultado.Data.email);
    }

    [Fact]
    public void CuandoBuscoDetalleUsuarioPorEmailValido_DebeRetornarUsuario_ConResultadoOk()
    {
        
        var mockService = new Mock<IUsuarioService>();
        var usuario = new UsuarioDto {  email = "correo@test.com", password = "abcdef", Rol = ERol.Organizador };

        mockService.Setup(s => s.DetalleUsuarioXEmail(usuario.email))
            .Returns(Result<Usuario?>.Ok(usuario));


        var resultado = mockService.Object.DetalleUsuarioXEmail(usuario.email);

       
        Assert.True(resultado.Success);
        Assert.Equal(EResultType.Ok, resultado.ResultType);
        Assert.Equal(usuario.email, resultado.Data.email);
        Assert.Equal(usuario.Rol, resultado.Data.Rol);
    }

    [Fact]
    public void CuandoActualizoElRolDeUnUsuario_DebeRetornarOk()
    {
       
        var mockService = new Mock<IUsuarioService>();
        var usuario = new Usuario { idUsuario = 3, email = "rol@test.com", passwordHash = "pass123", rol = ERol.Organizador };
        var nuevoRol = ERol.Administrador;

        mockService.Setup(s => s.ActualizarRol(usuario.idUsuario, nuevoRol))
            .Returns(Result<Usuario>.Ok());

       
        var resultado = mockService.Object.ActualizarRol(usuario.idUsuario, nuevoRol);

        
        Assert.True(resultado.Success);
        Assert.Equal(EResultType.Ok, resultado.ResultType);
    }

    [Fact]
    public void CuandoRealizoUnAltaDeUsuarioValido_DebeRetornarCreated()
    {
        
        var mockService = new Mock<IUsuarioService>();
        var usuarioDto = new UsuarioDto { email = "nuevo@correo.com", password = "segura123", Rol = ERol.Organizador };

        mockService.Setup(s => s.AltaUsuario(usuarioDto))
            .Returns(Result<Usuario>.Created(new Usuario
            {
                email = usuarioDto.email,
                passwordHash = usuarioDto.password,
                rol = usuarioDto.Rol
            }));

        
        var resultado = mockService.Object.AltaUsuario(usuarioDto);

        
        Assert.True(resultado.Success);
        Assert.Equal(EResultType.Created, resultado.ResultType);
        Assert.Equal(usuarioDto.email, resultado.Data.email);
        Assert.Equal(usuarioDto.Rol, resultado.Data.rol);
    }

    [Fact]
    public void CuandoRealizoUnAltaDeUsuarioInvalido_DebeRetornarBadRequest()
    {
        var mockRepoUsuario = new Mock<IRepoUsuario>();
        mockRepoUsuario.Setup(r => r.UniqueEmail(It.IsAny<string>())).Returns(false);

        var usuarioDto = new UsuarioDto
        {
            email = "correoInvalido",
            password = "123",
            Rol = (ERol)999
        };

        var validator = new UsuarioValidator(mockRepoUsuario.Object);

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

       
        Assert.False(resultado.Success);
        Assert.Equal(EResultType.BadRequest, resultado.ResultType);
        Assert.True(resultado.Errors.ContainsKey("email"));
        Assert.True(resultado.Errors.ContainsKey("password"));
        Assert.True(resultado.Errors.ContainsKey("Rol"));
    }
}