namespace SuperProyecto.Core.Entidades;

public class Evento
{
    public int idEvento { get; set; }
    public string nombre { get; set; }
    public string descripcion { get; set; }
    public DateTime fechaPublicacion { get; set; }
    public bool publicado { get; set; }
    public bool cancelado { get; set; }
    
    /* 
        CantaLali.
    */
}