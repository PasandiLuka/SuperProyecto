using Dapper;

using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.Entidades;

namespace SuperProyecto.Dapper;

public class RepoSector : Repo, IRepoSector
{
    public RepoSector(IAdo _ado) : base(_ado) { }

    private static readonly string _querySector
        = "SELECT * FROM Sector WHERE idLocal = @idLocal";
    public IEnumerable<Sector> GetSectores(int idLocal) => _conexion.Query<Sector>(_querySector, new { idLocal });
    
    private static readonly string _queryDetalleSector
        = @"SELECT * FROM Sector WHERE  idSector= @idSector";
    public Sector? DetalleSector(int IdSector)
    {
        return _conexion.QueryFirstOrDefault<Sector>(_queryDetalleSector, new { idSector = IdSector });
    }

    private static readonly string _queryAltaSector
        = @"INSERT INTO Sector (idLocal, nombre) VALUES (@idLocal, @nombre)";
    public void AltaSector(Sector sector, int idLocal)
    {
        _conexion.Execute(
            _queryAltaSector,
            new
            {
                idLocal,
                sector.nombre
            });
    }

    private static readonly string _queryUpdateSector
        = @"UPDATE Sector SET nombre = @nombre WHERE idSector = @idSector";
    public void UpdateSector(Sector sector, int id)
    {
        _conexion.Execute(
            _queryUpdateSector,
            new
            {
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
        = @"UPDATE Sector SET eliminado = true WHERE idSector = @idSector";
    public void DeleteSector(int id)
    {
        _conexion.Execute(_queryDeleteSector, new { id });
    }
}