namespace SuperProyecto.Core.Services.Persistencia;

public interface IRepoSector
{
    IEnumerable<Sector> GetSectores();
    Sector? DetalleSector(int idSector);
    void AltaSector(Sector sector);
    void UpdateSector(Sector sector, int id);
}