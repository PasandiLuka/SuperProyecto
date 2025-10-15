using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.DTO;

namespace SuperProyecto.Core.IServices;

public interface ISectorService
{
    IEnumerable<Sector> GetSectores();
    Sector? DetalleSector(int id);
    void UpdateSector(SectorDto sector, int id);
    void AltaSector(SectorDto sector);
    void DeleteSector(int id);
}