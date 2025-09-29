namespace SuperProyecto.Core.Persistencia;

public interface IRepoSector
{
    IEnumerable<Sector> GetSector();
    void AltaSector(Sector sector );
    Sector? DetalleSector(int idSector);
}