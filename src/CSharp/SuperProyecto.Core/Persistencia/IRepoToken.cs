using SuperProyecto.Core.Entidades;

namespace SuperProyecto.Core.Persistencia;

public interface IRepoToken
{
    void AltaRefreshToken(int idUsuario, string refreshToken, DateTime emitido, DateTime expiracion);
    RefreshToken? DetalleRefreshToken(string token);
    RefreshToken? DetalleRefreshTokenXEmision(DateTime emitido);
    void RevocarRefreshToken(string token);
}