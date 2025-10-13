using SuperProyecto.Core.Persistencia;

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

}