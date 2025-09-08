namespace SuperProyecto;

public class OrdenCompra
{
    public int NumeroOrden { get; set; }
    public DateTime FechaCompra { get; set; }
    public Cliente Cliente { get; set; }
    public List<Entrada> Entradas { get; set; }
    public decimal Total => Entradas.Sum(e => e.Precio);
}