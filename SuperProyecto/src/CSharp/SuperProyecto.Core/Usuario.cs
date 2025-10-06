using System.ComponentModel;
using SuperProyecto.Core.Enums;

namespace SuperProyecto.Core;

public class Usuario
{
    public int idUsuario { get; set; }
    public string email { get; set; }
    public string passwordHash { get; set; }
    public ERol rol { get; set; }
}