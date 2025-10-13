using SuperProyecto.Core.Enums;

namespace SuperProyecto.Services.DTO;

public class UsuarioDto
{
    public string email { get; set; }
    public string password { get; set; }
    public ERol Rol { get; set; }
}