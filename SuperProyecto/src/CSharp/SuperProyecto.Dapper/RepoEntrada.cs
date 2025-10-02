using System.Data;
using Dapper;

using SuperProyecto.Core.Services.Persistencia;
using SuperProyecto.Core;

namespace SuperProyecto.Dapper;

public class RepoEntrada : Repo, IRepoEntrada
{
    /* public RepoEntrada(IDbConnection conexion) : base(conexion) { }
    public RepoEntrada(string conexion) : base(conexion) { } */

    public RepoEntrada(IAdo _ado) : base(_ado) { }

    private static readonly string _queryEntradas
        = "SELECT * FROM Entradas";
    public IEnumerable<Entrada> GetEntradas() => _conexion.Query<Entrada>(_queryEntradas);

    private static readonly string _queryDetalleEntrada
        = @"SELECT * FROM Entradas WHERE idEntrada = @idEntrada";
    public Entrada? DetalleEntrada(int idEntrada) => _conexion.QueryFirstOrDefault<Entrada>(_queryDetalleEntrada, new { idEntrada });

    private static readonly string _queryAltaEntrada
        = @"INSERT INTO Entradas (idEntrada, idTarifa, numeroOrden, idFuncion, QR, usada) VALUES (@idEntrada, @idTarifa, @numeroOrden, @idFuncion, @QR, @usada)";
    public void AltaEntrada(Entrada entrada) => _conexion.Execute(_queryAltaEntrada, new { entrada.idEntrada, entrada.idTarifa, entrada.numeroOrden, entrada.idFuncion, entrada.QR, entrada.usada });

    private static readonly string _queryUpdateEntrada
        = @"UPDATE Entrada SET idTarifa = @unIdTarifa, numeroOrden = @unNumeroOrden, idFuncion = @unIdFuncion, QR = @unQR, usada = @unUsada WHERE idEntrada = @unIdEntrada";
    public void UpdateEntrada(Entrada entrada, int id)
    {
        _conexion.Execute(_queryUpdateEntrada, new { unIdEntrada = id, unNumeroOrden = entrada.numeroOrden, unIdFuncion = entrada.idFuncion, unQR = entrada.QR, unUsada = entrada.usada});
    }
}