namespace SuperProyecto.Core.Entidades;

public class Entrada
{
    public int idEntrada { get; set; }
    public int idOrden { get; set; }
    public int idTarifa { get; set; }
    public bool anulada { get; set; }
    public bool usada { get; set; }
}