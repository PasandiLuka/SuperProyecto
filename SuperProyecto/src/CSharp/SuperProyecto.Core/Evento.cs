namespace SuperProyecto.Core;

public class Evento
{
    public DateOnly fechaInicio { get; set; }
    public string? descripcion { get; set; }
    public Local? local { get; set; } //Ubicacion evento
    public List<Funcion>? funciones { get; set; }
    
    /* 
        CantaLali.
     */
}