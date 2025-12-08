using SuperProyecto.Core.DTO;
using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.Enums;

namespace SuperProyecto.Core.Persistencia;

public interface IUsuarioService
{
    Result<IEnumerable<Usuario>> GetUsuarios();
    Result<string[]> ObtenerRoles();
    Result<UsuarioDto> AltaUsuario(UsuarioDto usuarioDto);
    Result<Usuario> ActualizarRol(int id, int rol);
}