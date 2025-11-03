namespace SuperProyecto.Core.DTO;

public class OrdenDto
{
    public int idCliente { get; set; }
    public int idSector { get; set; }
    public DateTime fecha { get; set; }
    public bool pagada { get; set; }
    public bool cancelada { get; set; }
    public decimal total { get; set; }
}