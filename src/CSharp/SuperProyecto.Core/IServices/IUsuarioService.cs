using SuperProyecto.Core.DTO;
using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.Enums;

namespace SuperProyecto.Core.Persistencia;

public interface IUsuarioService
{
    public Usuario? DetalleUsuario(int id);
    Usuario? DetalleUsuarioXEmail(string email);
    void AltaUsuario(UsuarioDto usuario);
    void ActualizarRol(int id, ERol rol);
    bool UniqueEmail(string email);
}