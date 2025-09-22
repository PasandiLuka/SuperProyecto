namespace SuperProyecto.Core;

public class Evento
{
    public DateOnly FechaInicio { get; set; }
    public string? Descripcion { get; set; }
    public Local? local { get; set; } //Ubicacion evento
    public List<Funcion>? Funciones { get; set; }
    
    /* 
        CantaLali.
     */
}