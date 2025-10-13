using Dapper;

using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.Entidades;
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
        = @"INSERT INTO Entrada (idOrden) VALUES (@idOrden)";
    public void AltaEntrada(Entrada entrada)
    {
        _conexion.Execute(
            _queryAltaEntrada,
            new
            {
                entrada.idOrden
            });
    }

    private static readonly string _queryEntradaUsada
        = @"UPDATE Entrada SET usada = @usada WHERE idEntrada = @idEntrada";
    public void EntradaUsada(int idEntrada)
    {
        _conexion.Execute(
            _queryEntradaUsada,
            new
            {
                usada = true,
                idEntrada
            }
        );
    }
}