using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.Enums;

namespace SuperProyecto.Core.Persistencia;

public interface IRepoUsuario
{
    public Usuario? DetalleUsuario(int id);
    void AltaUsuario(Usuario usuario);
    void ActualizarRol(int id, ERol rol);
    bool UniqueEmail(string email);
    Usuario? DetalleUsuarioXEmail(string email);
    
}