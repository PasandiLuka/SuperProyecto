using SuperProyecto.Core.DTO;
using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.Persistencia;
using SuperProyecto.Services.Validators;
using MySqlConnector;

namespace SuperProyecto.Services.Service;

public class UsuarioService : IUsuarioService
{
    readonly IRepoUsuario _repoUsuario;
    readonly UsuarioValidator _validador;

    public Result<IEnumerable<Usuario>> GetUsuarios()
    {
        try
        {
            return Result<IEnumerable<Usuario>>.Ok(_repoUsuario.GetUsuarios());
        }
        catch (MySqlException)
        {
            return Result<IEnumerable<Usuario>>.Unauthorized();
        }
    }
    public UsuarioService(IRepoUsuario repoUsuario, UsuarioValidator validador)
    {
        _repoUsuario = repoUsuario;
        _validador = validador;
    }

    public Result<string[]> ObtenerRoles()
    {
        try
        {
            var resultado = Enum.GetNames(typeof(ERolDto));
            return Result<string[]>.Ok(resultado);  
        }
        catch (MySqlException)
        {
            return Result<string[]>.Unauthorized();
        }
    }

    public Result<Usuario> ActualizarRol(int id, int nuevoRol)
    {
        try
        {
            if (_repoUsuario.DetalleUsuario(id) is null) return Result<Usuario>.NotFound("El usuario solicitado no fue encontrado.");
            if (!((ERolDto)nuevoRol == ERolDto.Organizador) && !((ERolDto)nuevoRol == ERolDto.Cliente)|| !((ERolDto)nuevoRol == ERolDto.Cliente)) return Result<Usuario>.BadRequest(default, "El rol dado no se encuentra dentro de las opciones.");
            _repoUsuario.ActualizarRol(id, (ERolDto)nuevoRol);
            return Result<Usuario>.Ok();
        }
        catch (MySqlException)
        {
            return Result<Usuario>.Unauthorized();
        }
        
    }

    public Result<UsuarioDto> AltaUsuario(UsuarioDto usuarioDto)
    {
        try
        {
            var resultado = _validador.Validate(usuarioDto);
            if (!resultado.IsValid)
            {
                var listaErrores = resultado.Errors
                    .GroupBy(a => a.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    );
                return Result<UsuarioDto>.BadRequest(listaErrores);
            }
            var usuario = ConvertirDtoClase(usuarioDto);
            _repoUsuario.AltaUsuario(usuario);
            return Result<UsuarioDto>.Created(usuarioDto);
        }
        catch (MySqlException)
        {
            return Result<UsuarioDto>.Unauthorized();
        }
    }

    static Usuario ConvertirDtoClase(UsuarioDto usuarioDto)
    {
        return new Usuario
        {
            email = usuarioDto.email,
            passwordHash = AuthService.HashPassword(usuarioDto.password),
            rol = usuarioDto.Rol
        };
    }    
}

