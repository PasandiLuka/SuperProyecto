namespace SuperProyecto.Core.Services.Persistencia;

public interface IRepoSector
{
    IEnumerable<Sector> GetSector();
    void AltaSector(Sector sector);
    Sector? DetalleSector(int idSector);
    void UpdateSector(Sector sector, int id);
}