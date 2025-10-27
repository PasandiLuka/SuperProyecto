using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.DTO;

namespace SuperProyecto.Services.Service;

public class AuthService
{
    readonly IRepoUsuario _repoUsuario;
    readonly IRepoToken _repoToken;
    readonly TokenService _tokenService;
    public AuthService(IRepoUsuario repoUsuario, IRepoToken repoToken, TokenService tokenService)
    {
        _repoUsuario = repoUsuario;
        _repoToken = repoToken;
        _tokenService = tokenService;
    }
    
    public void RefreshToken(RefreshTokenRequest model)
    {
        var tokenInfo = _repoToken.DetalleRefreshToken(model.refreshToken);
        if (tokenInfo == null || tokenInfo.revocado || tokenInfo.expiracion < DateTime.UtcNow) return;
        var usuario = _repoUsuario.DetalleUsuario(tokenInfo.idUsuario);
        var tokens = _tokenService.GenerarTokens(usuario);
        _repoToken.RevocarRefreshToken(model.refreshToken);
        _repoToken.AltaRefreshToken(usuario.idUsuario, tokens.refreshToken, DateTime.UtcNow.AddDays(7));
    }
}