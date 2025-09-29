namespace SuperProyecto.Core;

public class Entrada
{
    public int idEntrada { get; set; }
    public int idTarifa { get; set; }
    public Tarifa tarifa { get; set; }
    public string QR { get; set; }
    public Funcion Funcion { get; set; }
    public bool Usada { get; set; } 
}