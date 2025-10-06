using Dapper;
using SuperProyecto.Core;
using SuperProyecto.Core.Persistencia;

namespace SuperProyecto.Dapper;

public class RepoQr : Repo, IRepoQr
{
    public RepoQr(IAdo _ado) : base(_ado) { }

    private static readonly string _queryAltaQr
        = @"INSERT INTO Qr (idEntrada, url) VALUES (@idEntrada, @url)";
    public void AltaQr(int idEntrada, string url)
    {
        _conexion.Execute(_queryAltaQr, new { idEntrada, url });
    }

    private static readonly string _queryDetalleQr
        = @"SELECT * FROM Qr WHERE idQr = @idQr";
    public Qr? DetalleQr(int idQr)
    {
        return _conexion.QueryFirstOrDefault<Qr>(_queryDetalleQr, new { idQr });
    }
}