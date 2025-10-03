namespace SuperProyecto.Core;

public class Usuario : Cliente
{
    public int idUsuario { get; set; }
    public string username { get; set; }
    public string passwordHash { get; set; }
}