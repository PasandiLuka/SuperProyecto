using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.DTO;
using SuperProyecto.Core.Entidades;

using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace SuperProyecto.Services.Service;

public class AuthService
{
    readonly IRepoUsuario _repoUsuario;
    readonly IRepoToken _repoToken;
    readonly TokenService _tokenService;
    readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(IHttpContextAccessor httpContextAccessor, IRepoUsuario repoUsuario, IRepoToken repoToken, TokenService tokenService)
    {
        _httpContextAccessor = httpContextAccessor;
        _repoUsuario = repoUsuario;
        _repoToken = repoToken;
        _tokenService = tokenService;
    }

    public Result<TokenResponseDto> Login(LoginRequest model)
    {
        var usuario = _repoUsuario.DetalleUsuarioXEmail(model.email);
        if (usuario == null) return Result<TokenResponseDto>.BadRequest(default, "Usuario o contraseña incorrectos.");
        if (!VerificarPassword(model.password, usuario.passwordHash))
            return Result<TokenResponseDto>.BadRequest(default, "Usuario o contraseña incorrectos.");
        var tokens = _tokenService.GenerarTokens(usuario);
        _repoToken.AltaRefreshToken(usuario.idUsuario, tokens.refreshToken, DateTime.UtcNow.AddDays(7));
        return Result<TokenResponseDto>.Ok(tokens);
    }

    public Result<AuthMeDto> Me()
    {
        var User = _httpContextAccessor.HttpContext?.User;
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim is null) return Result<AuthMeDto>.Unauthorized();
        var usuario = _repoUsuario.DetalleUsuario(int.Parse(userIdClaim));
        if (usuario is null) return Result<AuthMeDto>.BadRequest();
        var result = new AuthMeDto
        {
            email = usuario.email,
            rol = usuario.rol.ToString()
        };
        return Result<AuthMeDto>.Ok(result);
    }

    public Result<TokenResponseDto> RefreshToken(RefreshTokenRequest model)
    {
        var tokenInfo = _repoToken.DetalleRefreshToken(model.refreshToken);
        if (tokenInfo == null || tokenInfo.revocado || tokenInfo.expiracion < DateTime.UtcNow) return Result<TokenResponseDto>.BadRequest(default, "Refresh token no valido.");
        var usuario = _repoUsuario.DetalleUsuario(tokenInfo.idUsuario);
        var tokens = _tokenService.GenerarTokens(usuario);
        _repoToken.RevocarRefreshToken(model.refreshToken);
        _repoToken.AltaRefreshToken(usuario.idUsuario, tokens.refreshToken, DateTime.UtcNow.AddDays(7));
        return Result<TokenResponseDto>.Ok(tokens);
    }

    // Helpers para password
    static string HashPassword(string password)
    {
        using var sha = SHA256.Create();
        var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
        return BitConverter.ToString(hash).Replace("-", "").ToLower();
    }

    static bool VerificarPassword(string password, string hash)
    {
        return HashPassword(password) == hash;
    }
}