using System.Data;
using Dapper;

using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core;
using System.Formats.Tar;

namespace SuperProyecto.Dapper;

public class RepoTarifa : Repo, IRepoTarifa
{
    public RepoTarifa(IAdo _ado) : base(_ado) { }

    private static readonly string _queryTarifa
        = "SELECT * FROM Tarifa";
    public IEnumerable<Tarifa> GetTarifa() => _conexion.Query<Tarifa>(_queryTarifa);
    
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
        = @"INSERT INTO Tarifa (idFuncion, nombre, precio, stock) VALUES (@idFuncion, @nombre, @precio, @stock)";
    public void AltaTarifa(Tarifa tarifa)
    {
        _conexion.Execute(
            _queryAltaTarifa,
            new
            {
                tarifa.idTarifa,
                tarifa.idFuncion,
                tarifa.nombre,
                tarifa.precio,
                tarifa.stock
            });
    }

    private static readonly string _queryUpdateTarifa
        = @"UPDATE Sector SET idFuncion = @idFuncion, nombre = @nombre, precio = @precio, stock = @stock WHERE idTarifa = @idTarifa";
    public void UpdateTarifa(Tarifa tarifa, int id)
    {
        _conexion.Execute(
            _queryUpdateTarifa,
            new
            {
                tarifa.idFuncion,
                tarifa.nombre,
                tarifa.precio,
                tarifa.stock,
                idTarifa = id
            });
    }
}