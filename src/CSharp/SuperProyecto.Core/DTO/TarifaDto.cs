namespace SuperProyecto.Core.DTO;

public class TarifaDto
{
    public decimal PrecioUnitario { get; set; }
    public int stock { get; set; }
    public bool activo { get; set; }
    public int descuento { get; set; }
}