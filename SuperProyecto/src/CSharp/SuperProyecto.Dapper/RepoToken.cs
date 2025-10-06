using Dapper;
using SuperProyecto.Core.DTO;
using SuperProyecto.Core;
using SuperProyecto.Core.Persistencia;

namespace SuperProyecto.Dapper;

public class RepoToken : Repo, IRepoToken
{
    public RepoToken(IAdo ado) : base(ado) {}

    private static readonly string _queryDetalleRefreshToken
        = @"SELECT * FROM RefreshTokens WHERE token = @token";
    public RefreshToken? DetalleRefreshToken(string token)
    {
        return _conexion.QueryFirstOrDefault<RefreshToken>(
            _queryDetalleRefreshToken,
            new
            {
                token
            }
        );
    }

    private static readonly string _queryAltaRefreshToken
        = @"INSERT INTO RefreshTokens (idUsuario, token, expiracion) VALUES (@idUsuario, @token, @expiracion)";
    public void AltaRefreshToken(int idUsuario, string token, DateTime expiracion)
    {
        _conexion.Execute(
            _queryAltaRefreshToken,
            new
            {
                idUsuario,
                token,
                expiracion
            });
    }

    private static readonly string _queryRevocarRefreshToken
        = @"UPDATE RefreshTokens SET revocado = 1 WHERE token = @token";
    public void RevocarRefreshToken(string token)
    {
        _conexion.Execute(
            _queryRevocarRefreshToken,
            new
            {
                token
            }
        );
    }
}