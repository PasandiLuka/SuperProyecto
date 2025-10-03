using System.Data;
using Dapper;

using SuperProyecto.Core.Services.Persistencia;
using SuperProyecto.Core;

namespace SuperProyecto.Dapper;

public class RepoSector : Repo, IRepoSector
{
    public RepoSector(IAdo _ado) : base(_ado) { }

    private static readonly string _querySector
        = "SELECT * FROM Sector";
    public IEnumerable<Sector> GetSectores() => _conexion.Query<Sector>(_querySector);
    
    private static readonly string _queryDetalleSector
        = @"SELECT * FROM Sector WHERE  idSector= @idSector";
    public Sector? DetalleSector(int IdSector)
    {
        return _conexion.QueryFirstOrDefault<Sector>(_queryDetalleSector, new { idSector = IdSector });
    }

    private static readonly string _queryAltaSector
        = @"INSERT INTO Sector (idLocal, nombre, capacidad) VALUES (@idLocal, @nombre, @capacidad)";
    public void AltaSector(Sector sector)
    {
        _conexion.Execute(
            _queryAltaSector,
            new
            {
                sector.idLocal,
                sector.nombre,
                sector.capacidad
            });
    }

    private static readonly string _queryUpdateSector
        = @"UPDATE Sector SET idLocal = @idLocal, nombre = @nombre, capacidad = @capacidad WHERE idSector = @idSector";
    public void UpdateSector(Sector sector, int id)
    {
        _conexion.Execute(
            _queryUpdateSector,
            new
            {
                sector.idLocal,
                sector.nombre,
                sector.capacidad,
                idSector = id
            });
    }

    private static readonly string _queryDeleteSector
        = @"DELETE FROM Sector WHERE idSector = @idSector";
    public void DeleteSector(int id)
    {
        _conexion.Execute(_queryDeleteSector, new { id });
    }
}