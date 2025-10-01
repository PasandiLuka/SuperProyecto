namespace SuperProyecto.Core;

public class Orden
{
    public int numeroOrden { get; set; }
    public int DNI { get; set; }
    public DateTime fechaCompra { get; set; }
    public Cliente Cliente { get; set; }
    public List<Entrada> Entradas { get; set; }
    public decimal precioTotal => Entradas.Sum(e => e.tarifa.precio);
}