using System.Data;
using Dapper;

using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core;

namespace SuperProyecto.Dapper;

public class RepoSector : Repo, IRepoSector
{
    public RepoSector(IDbConnection conexion) : base(conexion) { }

    private static readonly string _querySector
        = "SELECT * FROM Sector";
    public IEnumerable<Sector> GetSector() => _conexion.Query<Sector>(_querySector);

    private static readonly string _queryAltaSector
        = @"INSERT INTO Sector (idSector, sector) VALUES (@idSector, @sector)";
    public void AltaSector(Sector sector)
    {
        _conexion.Execute(_queryAltaSector, new { idSector = sector.idSector, sector = sector.sector });
    }

    private static readonly string _queryDetalleSector
        = @"SELECT * FROM Sector WHERE  idSector= @idSector";
    public Sector? DetalleSector(int IdSector)
    {
        return _conexion.QueryFirstOrDefault<Sector>(_queryDetalleSector, new { idSector = IdSector });
    }
}