using Dapper;

using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.Entidades;

namespace SuperProyecto.Dapper;

public class RepoTarifa : Repo, IRepoTarifa
{
    public RepoTarifa(IAdo _ado) : base(_ado) { }

    private static readonly string _queryTarifa
        = "SELECT * FROM Tarifa";
    public IEnumerable<Tarifa> GetTarifas() => _conexion.Query<Tarifa>(_queryTarifa);
    
    
    private static readonly string _queryDetalleTarifa
        = @"SELECT * FROM Tarifa WHERE idTarifa= @idTarifa";
    public Tarifa? DetalleTarifa(int IdTarifa)
    {
        return _conexion.QueryFirstOrDefault<Tarifa>(
            _queryDetalleTarifa,
            new
            {
                idTarifa = IdTarifa
            });
    }

    private static readonly string _queryAltaTarifa
        = @"INSERT INTO Tarifa (idFuncion, idSector, precio, stock) VALUES (@idFuncion, @idSector, @precio, @stock)";
    public void AltaTarifa(Tarifa tarifa)
    {
        _conexion.Execute(
            _queryAltaTarifa,
            new
            {
                tarifa.idFuncion,
                tarifa.idSector,
                tarifa.precio,
                tarifa.stock
            });
    }

    private static readonly string _queryUpdateTarifa
        = @"UPDATE Tarifa SET idFuncion = @idFuncion, idSector = @idSector, precio = @precio, stock = @stock WHERE idTarifa = @idTarifa";
    public void UpdateTarifa(Tarifa tarifa, int id)
    {
        _conexion.Execute(
            _queryUpdateTarifa,
            new
            {
                tarifa.idFuncion,
                tarifa.idSector,
                tarifa.precio,
                tarifa.stock,
                idTarifa = id
            });
    }
}