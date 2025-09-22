namespace SuperProyecto.Core;

public class Local
{
    public int IdLocal { get; set; }
    public string nombre { get; private set; }
    public string direccion { get; private set; }
    public int CapacidadMax { get; private set; }
    public List<Sector> Sectores { get; set; } = new List<Sector>(); 
    /* 
    Lali canta en la rural
        */
}