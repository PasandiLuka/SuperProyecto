using System.Data;
using Dapper;

using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core;

namespace SuperProyecto.Dapper;

public class RepoEntrada : Repo, IRepoEntrada
{
    public RepoEntrada(IDbConnection conexion) : base(conexion) { }

    private static readonly string _queryEntradas
        = "SELECT * FROM Entradas";
    public IEnumerable<Entrada> GetEntradas() => _conexion.Query<Entrada>(_queryEntradas);

    private static readonly string _queryAltaEntrada
        = @"INSERT INTO Entradas (idEntrada, idTarifa, numeroOrden, idFuncion, QR, usada) VALUES (@idEntrada, @idTarifa, @numeroOrden, @idFuncion, @QR, @usada)";
    public void AltaEntrada(Entrada entrada) => _conexion.Execute(_queryAltaEntrada, new { entrada.idEntrada, entrada.idTarifa, entrada.numeroOrden, entrada.idFuncion, entrada.QR, entrada.usada });

    private static readonly string _queryDetalleEntrada
        = @"SELECT * FROM Entradas WHERE idEntrada = @idEntrada";
    public Entrada? DetalleEntrada(int idEntrada) => _conexion.QueryFirstOrDefault<Entrada>(_queryDetalleEntrada, new { idEntrada });
}