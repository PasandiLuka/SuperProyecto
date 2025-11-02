namespace SuperProyecto.Core.Entidades;

public class Cliente
{
    public int idCliente { get; set; }
    public int DNI { get; set; }
    public int idUsuario { get; set; }
    public string nombre { get; set; }
    public string apellido { get; set; }
    public int telefono { get; set; }
}