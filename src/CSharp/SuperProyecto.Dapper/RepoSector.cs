using Dapper;

using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.Entidades;

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
        = @"INSERT INTO Sector (idLocal, nombre) VALUES (@idLocal, @nombre)";
    public void AltaSector(Sector sector)
    {
        _conexion.Execute(
            _queryAltaSector,
            new
            {
                sector.idLocal,
                sector.nombre
            });
    }

    private static readonly string _queryUpdateSector
        = @"UPDATE Sector SET idLocal = @idLocal, nombre = @nombre WHERE idSector = @idSector";
    public void UpdateSector(Sector sector, int id)
    {
        _conexion.Execute(
            _queryUpdateSector,
            new
            {
                sector.idLocal,
                sector.nombre,
                idSector = id
            });
    }
    private static readonly string _queryDetalleSectorDeleteLocal
        = @"SELECT * FROM Sector WHERE idLocal = @idLocal";
    public Sector? DetalleSectorDeleteLocal(int idLocal)
    {
        return _conexion.QueryFirstOrDefault(
            _queryDetalleSectorDeleteLocal,
            new
            {
                idLocal
            }
        );
    }

    private static readonly string _queryDeleteSector
        = @"DELETE FROM Sector WHERE idSector = @idSector";
    public void DeleteSector(int id)
    {
        _conexion.Execute(_queryDeleteSector, new { id });
    }
}