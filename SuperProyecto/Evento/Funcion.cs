namespace SuperProyecto;

public class Funcion
{
    public string Nombre { get; set; }
    public DateTime FechaHora { get; set; }
    public int Capacidad { get; set; }
    public Evento Evento { get; set; }
    public List<Entrada> Entradas { get; set; }
}