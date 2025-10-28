using SuperProyecto.Core.DTO;
using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.Enums;
using SuperProyecto.Services.Validators;
using FluentValidation.Validators;

namespace SuperProyecto.Services.Service;

public class UsuarioService : IUsuarioService
{
    readonly IRepoUsuario _repoUsuario;
    readonly UsuarioValidator _validador;

    public UsuarioService(IRepoUsuario repoUsuario, UsuarioValidator validador)
    {
        _repoUsuario = repoUsuario;
        _validador = validador;
    }

    public Result<string[]> ObtenerRoles()
    {
        var resultado = Enum.GetNames(typeof(ERol));
        return Result<string[]>.Ok(resultado);
    }

    public Result<Usuario> ActualizarRol(int id, ERol nuevoRol)
    {
        if (_repoUsuario.DetalleUsuario(id) is null) return Result<Usuario>.NotFound("El usuario solicitado no fue encontrado.");
        if (nuevoRol != ERol.Administrador || nuevoRol != ERol.Cliente || nuevoRol != ERol.Organizador) return Result<Usuario>.BadRequest();
        _repoUsuario.ActualizarRol(id, nuevoRol);
        return Result<Usuario>.Ok();
    }

    public Result<UsuarioDto> AltaUsuario(UsuarioDto usuarioDto)
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