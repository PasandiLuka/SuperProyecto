namespace SuperProyecto.Core;

public class Local
{
    public int idLocal { get; set; }
    public string direccion { get;  set; }
    public int capacidadMax { get; set; }
    public List<Sector> sectores { get; set; } = new List<Sector>(); 
    /* 
    Lali canta en la rural
        */
}