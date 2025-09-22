using Microsoft.VisualBasic;

namespace SuperProyecto.Core;

public class Entrada
{
    public Tarifa tarifa { get; set; }
    public string QR { get; set; }
    public Funcion Funcion { get; set; }
    public bool Usada { get; set; } 
}