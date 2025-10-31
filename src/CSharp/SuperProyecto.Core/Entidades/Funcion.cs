namespace SuperProyecto.Core.Entidades;

public class Funcion
{
    public int idFuncion { get; set; }
    public int idEvento { get; set; }
    /* public int idTarifa { get; set; } */
    public DateTime fechaHora { get; set; }
    public int stock { get; set; }
    public bool cancelada { get; set; }

    /* 
        CantaLali a las:
        15hs
        19hs
        22hs
     */
}