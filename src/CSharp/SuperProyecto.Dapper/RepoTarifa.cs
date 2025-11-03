using Dapper;

using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.DTO;

namespace SuperProyecto.Dapper;

public class RepoTarifa : Repo, IRepoTarifa
{
    public RepoTarifa(IAdo _ado) : base(_ado) { }

    private static readonly string _queryTarifa
        = "SELECT * FROM Tarifa WHERE idFuncion = @idFuncion";
    public IEnumerable<Tarifa> GetTarifas(int idFuncion) => _conexion.Query<Tarifa>(_queryTarifa, new { idFuncion });
    
    
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
        = @"UPDATE Tarifa SET precio = @precio, stock = @stock, activo = @activo WHERE idTarifa = @idTarifa";
    public void UpdateTarifa(TarifaDto tarifa, int id)
    {
        _conexion.Execute(
            _queryUpdateTarifa,
            new
            {
                tarifa.precio,
                tarifa.stock,
                tarifa.activo,
                idTarifa = id
            });
    }

    private static readonly string _queryDetalleOrdenDeleteSector
        = @"SELECT * FROM Tarifa WHERE idSector = @idSector";
    public Tarifa? DetalleTarifaDeleteSector(int idSector)
    {
        return _conexion.QueryFirstOrDefault(
            _queryDetalleOrdenDeleteSector,
            new
            {
                idSector
            }
        );
    }
}