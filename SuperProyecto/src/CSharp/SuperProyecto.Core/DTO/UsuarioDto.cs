namespace SuperProyecto.Core.DTO;

public class UsuarioDto
{
    public int idUsuario { get; set; }
    public int idCliente { get; set; }
    public string username { get; set; }
    public string passwordHash { get; set; }
}