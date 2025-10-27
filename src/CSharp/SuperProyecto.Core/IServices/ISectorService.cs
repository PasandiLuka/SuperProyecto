using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.DTO;

namespace SuperProyecto.Core.IServices;

public interface ISectorService
{
    Result<IEnumerable<Sector>> GetSectores();
    Result<Sector?> DetalleSector(int id);
    Result<SectorDto> UpdateSector(SectorDto sector, int id);
    Result<SectorDto> AltaSector(SectorDto sector);
    Result<SectorDto> DeleteSector(int id);
}