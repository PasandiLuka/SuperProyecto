using SuperProyecto.Core.Entidades;

namespace SuperProyecto.Core.IServices;

public interface ISectorService
{
    IEnumerable<Sector> GetSectores();
    Sector? DetalleSector(int id);
    void UpdateSector(Sector sector, int id);
    void AltaSector(Sector sector);
    void DeleteSector(int id);
}