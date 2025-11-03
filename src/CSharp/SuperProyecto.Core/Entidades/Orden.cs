namespace SuperProyecto.Core.Entidades;

public class Orden
{
    public int idOrden { get; set; }
    public int idCliente { get; set; }
    public int idSector { get; set; }
    public DateTime fecha { get; set; }
    public bool pagada { get; set; }
    public bool cancelada { get; set; }
    public decimal total { get; set; }
}