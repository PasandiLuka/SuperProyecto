namespace SuperProyecto.Core.DTO;

public class TarifaDto
{
    public int idFuncion { get; set; }
    public int idSector { get; set; }
    public decimal precio { get; set; }
    public int stock { get; set; }
    public bool activo { get; set; }
}