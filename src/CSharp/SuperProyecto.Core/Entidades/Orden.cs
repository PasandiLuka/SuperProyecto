namespace SuperProyecto.Core.Entidades;

public class Orden
{
    public int idOrden { get; set; }
    public int DNI { get; set; }
    public int idSector { get; set; }//
    public DateTime fecha { get; set; }
    public bool pagada { get; set; }
}