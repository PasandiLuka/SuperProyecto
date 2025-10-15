using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.IServices;
using SuperProyecto.Core.DTO;

namespace SuperProyecto.Services.Service;

public class SectorService : ISectorService
{
    readonly IRepoSector _repoSector;
    public SectorService(IRepoSector repoSector)
    {
        _repoSector = repoSector;
    }

    public IEnumerable<Sector> GetSectores() => _repoSector.GetSectores();

    public Sector? DetalleSector(int id) => _repoSector.DetalleSector(id);

    public void AltaSector(SectorDto sectorDto)
    {
        var sector = ConvertirDtoClase(sectorDto);
        _repoSector.AltaSector(sector);
    } 

    public void UpdateSector(SectorDto sectorDto, int id)
    {
        var sector = ConvertirDtoClase(sectorDto);
        _repoSector.UpdateSector(sector, id);
    }

    public void DeleteSector(int id) => _repoSector.DeleteSector(id);
    
    static Sector ConvertirDtoClase(SectorDto sectorDto)
    {
        return new Sector
        {
            idLocal = sectorDto.idLocal,
            nombre = sectorDto.nombre
        };
    }
}