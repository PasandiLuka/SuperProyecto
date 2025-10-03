namespace SuperProyecto.Core;

public class Sector
{
    public int idSector { get; set; }
    public int idLocal { get; set; }
    public string nombre { get; set; }
    public int capacidad { get; set; }

    public Funcion funcion { get; set; }
    
    /* 
        Platea
        Popular
        Sector Vip
        */
}