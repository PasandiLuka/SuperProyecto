using SuperProyecto.Core.Entidades;

namespace SuperProyecto.Core.Persistencia;

public interface IRepoSector
{
    Sector? DetalleSectorDeleteLocal(int idLocal);
    IEnumerable<Sector> GetSectores();
    Sector? DetalleSector(int idSector);
    void AltaSector(Sector sector);
    void UpdateSector(Sector sector, int id);
    void DeleteSector(int idSector);
}