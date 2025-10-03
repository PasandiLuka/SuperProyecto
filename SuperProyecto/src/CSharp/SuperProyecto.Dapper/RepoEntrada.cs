using System.Data;
using Dapper;

using SuperProyecto.Core.Services.Persistencia;
using SuperProyecto.Core;

namespace SuperProyecto.Dapper;

public class RepoEntrada : Repo, IRepoEntrada
{
    public RepoEntrada(IAdo _ado) : base(_ado) { }

    private static readonly string _queryEntradas
        = "SELECT * FROM Entrada";
    public IEnumerable<Entrada> GetEntradas() => _conexion.Query<Entrada>(_queryEntradas);

    private static readonly string _queryDetalleEntrada
        = @"SELECT * FROM Entrada WHERE idEntrada = @idEntrada";
    public Entrada? DetalleEntrada(int idEntrada) => _conexion.QueryFirstOrDefault<Entrada>(_queryDetalleEntrada, new { idEntrada });

    private static readonly string _queryAltaEntrada
        = @"INSERT INTO Entrada (idOrden, idFuncion, codigoQr, usada) VALUES (@idOrden, @idFuncion, @codigoQr, @usada)";
    public void AltaEntrada(Entrada entrada) => _conexion.Execute(_queryAltaEntrada, new { entrada.idOrden, entrada.idFuncion, entrada.codigoQr, entrada.usada});

    private static readonly string _queryUpdateEntrada
        = @"UPDATE Entrada SET idTarifa = @unIdTarifa, idFuncion = @unIdFuncion, QR = @unQR, usada = @unUsada WHERE idEntrada = @unIdEntrada";
    public void UpdateEntrada(Entrada entrada, int id)
    {
        _conexion.Execute(
            _queryUpdateEntrada,
            new
            {
                unIdEntrada = id,
                unIdFuncion = entrada.idFuncion,
                unQR = entrada.codigoQr,
                unUsada = entrada.usada
            });
    }
}