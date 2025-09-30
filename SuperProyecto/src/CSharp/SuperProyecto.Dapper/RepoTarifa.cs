using System.Data;
using Dapper;

using SuperProyecto.Core.Services.Persistencia;
using SuperProyecto.Core;

namespace SuperProyecto.Dapper;

public class RepoTarifa : Repo, IRepoTarifa
{
    public RepoTarifa(IDbConnection conexion) : base(conexion) { }

    private static readonly string _queryTarifa
        = "SELECT * FROM Tarifa";
    public IEnumerable<Tarifa> GetTarifa() => _conexion.Query<Tarifa>(_queryTarifa);

    private static readonly string _queryAltaTarifa
        = @"INSERT INTO Tarifa (idTarifa, precio) VALUES (@idTarifa, @precio)";
    public void AltaTarifa(Tarifa tarifa)
    {
        _conexion.Execute(_queryAltaTarifa, new { idTarifa = tarifa.idTarifa, precio = tarifa.precio });
    }

    private static readonly string _queryDetalleTarifa
        = @"SELECT * FROM Tarifa WHERE idTarifa= @idTarifa";
    public Tarifa? DetalleTarifa(int IdTarifa)
    {
        return _conexion.QueryFirstOrDefault<Tarifa>(_queryDetalleTarifa, new { idTarifa = IdTarifa });
    }

    private static readonly string _queryUpdateTarifa
        = @"UPDATE Sector SET idTarifa = @unIdTarifa, precio = @unPrecio";
    public void UpdateTarifa(Tarifa tarifa, int id)
    {
        _conexion.Execute(_queryUpdateTarifa, new { unIdTarifa = tarifa.idTarifa, unPrecio = tarifa.precio });
    }
}