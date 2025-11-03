using SuperProyecto.Core.Entidades;

namespace SuperProyecto.Core.Persistencia;

public interface IRepoSector
{
    Sector? DetalleSectorDeleteLocal(int idLocal);
    IEnumerable<Sector> GetSectores(int idLocal);
    Sector? DetalleSector(int idSector);
    void AltaSector(Sector sector, int idLocal);
    void UpdateSector(Sector sector, int id);
    void DeleteSector(int idSector);
}