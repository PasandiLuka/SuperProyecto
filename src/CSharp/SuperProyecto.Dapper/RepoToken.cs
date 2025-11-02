using Dapper;

using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.Persistencia;

namespace SuperProyecto.Dapper;

public class RepoToken : Repo, IRepoToken
{
    public RepoToken(IAdo ado) : base(ado) {}

    private static readonly string _queryDetalleRefreshToken
        = @"SELECT * FROM RefreshTokens WHERE refreshToken = @refreshToken";
    public RefreshToken? DetalleRefreshToken(string refreshToken)
    {
        return _conexion.QueryFirstOrDefault<RefreshToken>(
            _queryDetalleRefreshToken,
            new
            {
                refreshToken
            }
        );
    }

    private static readonly string _queryDetalleRefreshTokenXEmision
        = @"SELECT * FROM RefreshTokens WHERE emitido = @emitido";
    public RefreshToken? DetalleRefreshTokenXEmision(DateTime emitido)
    {
        return _conexion.QueryFirstOrDefault<RefreshToken>(
            _queryDetalleRefreshTokenXEmision,
            new
            {
                emitido
            }
        );
    }

    private static readonly string _queryAltaRefreshToken
        = @"INSERT INTO RefreshTokens (idUsuario, refreshToken, emitido, expiracion) VALUES (@idUsuario, @refreshToken, @emitido, @expiracion)";
    public void AltaRefreshToken(int idUsuario, string refreshToken, DateTime emitido, DateTime expiracion)
    {
        _conexion.Execute(
            _queryAltaRefreshToken,
            new
            {
                idUsuario,
                refreshToken,
                emitido,
                expiracion
            });
    }

    private static readonly string _queryRevocarRefreshToken
        = @"UPDATE RefreshTokens SET revocado = 1 WHERE refreshToken = @refreshToken";
    public void RevocarRefreshToken(string refreshToken)
    {
        _conexion.Execute(
            _queryRevocarRefreshToken,
            new
            {
                refreshToken
            }
        );
    }
}