using SuperProyecto.Core.Entidades;

namespace SuperProyecto.Core.Persistencia;

public interface IRepoToken
{
    void AltaRefreshToken(int idUsuario, string token, DateTime expiracion);
    RefreshToken? DetalleRefreshToken(string token);
    void RevocarRefreshToken(string token);
}