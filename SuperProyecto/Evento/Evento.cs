namespace SuperProyecto;

public class Evento
{
    public DateTime FechaHoraI { get; set; }
    public int CapacidadMax { get; set; }
    public string Descripcion { get; set; }
    public string Ubicacion { get; set; }
    public List<Funcion> Funciones { get; set; }
}