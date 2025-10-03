namespace SuperProyecto.Core;

public class Orden
{
    public int idOrden { get; set; }
    public int DNI { get; set; }
    public DateTime fecha { get; set; }
    public decimal total { get; set; }
}