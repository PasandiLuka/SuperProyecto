using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.IServices;

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

    public void AltaSector(Sector sector) => _repoSector.AltaSector(sector);

    public void UpdateSector(Sector sector, int id) => _repoSector.UpdateSector(sector, id);
    
    public void DeleteSector(int id) => _repoSector.DeleteSector(id);
}