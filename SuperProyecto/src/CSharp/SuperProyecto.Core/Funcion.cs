namespace SuperProyecto.Core;

public class Funcion
{
    public int idFuncion { get; set; }
    public int idEvento { get; set; }
    public string descripcion { get; set; }
    public DateTime fechaHora { get; set; }
    public List<Entrada> entradas { get; set; }

    /* 
        CantaLali a las:
        15hs
        19hs
        22hs
     */
}