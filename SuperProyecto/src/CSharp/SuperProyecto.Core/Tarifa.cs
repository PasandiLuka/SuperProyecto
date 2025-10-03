namespace SuperProyecto.Core;

public class Tarifa
{
    public int idTarifa { get; set; }
    public int idFuncion { get; set; }
    public string nombre { get; set; }
    public decimal precio { get; set; }
    public int stock { get; set; }
}