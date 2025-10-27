using SuperProyecto.Core.DTO;
using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.Enums;

namespace SuperProyecto.Services.Service;

public class UsuarioService : IUsuarioService
{
    readonly IRepoUsuario _repoUsuario;
    public UsuarioService(IRepoUsuario repoUsuario)
    {
        _repoUsuario = repoUsuario;
    }

    public Result<string[]> ObtenerRoles()
    {
        var resultado = Enum.GetNames(typeof(ERol));
        return Result<string[]>.Ok(resultado);
    }

    public Result<Usuario> ActualizarRol(int id, ERol nuevoRol)
    {
        _repoUsuario.ActualizarRol(id, nuevoRol);
        return Result<Usuario>.Ok();
    }

    public Result<Usuario> AltaUsuario(UsuarioDto usuarioDto)
    {
        var usuario = ConvertirDtoClase(usuarioDto);
        _repoUsuario.AltaUsuario(usuario);
        return Result<Usuario>.Created(usuario);
    }

    static Usuario ConvertirDtoClase(UsuarioDto usuarioDto)
    {
        return new Usuario
        {
            email = usuarioDto.email,
            passwordHash = usuarioDto.password,
            rol = usuarioDto.Rol
        };
    }
}