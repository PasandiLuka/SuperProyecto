namespace SuperProyecto;

public class Evento
{
    public Date FechaInicio { get; set; }
    public string? Descripcion { get; set; }
    public Local? local { get; set; } //Ubicacion evento
    public List<Funcion>? Funciones { get; set; }
    
    /* 
        CantaLali.
     */
}