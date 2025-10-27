using SuperProyecto.Core.DTO;
using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.Enums;

namespace SuperProyecto.Core.Persistencia;

public interface IUsuarioService
{
    Result<string[]> ObtenerRoles();
    Result<Usuario> AltaUsuario(UsuarioDto usuarioDto);
    Result<Usuario> ActualizarRol(int id, ERol rol);
}