using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.DTO;
using SuperProyecto.Core.Entidades;

using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using MySqlConnector;
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
        try
        {    
            var usuario = _repoUsuario.DetalleUsuarioXEmail(model.email);
            if (usuario == null) return Result<TokenResponseDto>.BadRequest(default, "Usuario o contraseña incorrectos.");
            if (!VerificarPassword(model.password, usuario.passwordHash))
                return Result<TokenResponseDto>.BadRequest(default, "Usuario o contraseña incorrectos.");
            var tokens = _tokenService.GenerarTokens(usuario);
            _repoToken.AltaRefreshToken(usuario.idUsuario, tokens.refreshToken, tokens.emitido, DateTime.UtcNow.AddDays(7));
            return Result<TokenResponseDto>.Ok(tokens);
        }
        catch (MySqlException)
        {
            return Result<TokenResponseDto>.Unauthorized();
        }
    }

    public Result<AuthMeDto> Me()
    {
        try
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
        catch (MySqlException)
        {
            return Result<AuthMeDto>.Unauthorized();
        }
    }

    public Result<TokenResponseDto> RefreshToken(RefreshTokenRequest model)
    {
        try
        {
            var tokenInfo = _repoToken.DetalleRefreshToken(model.refreshToken);
            if (tokenInfo == null || tokenInfo.revocado || tokenInfo.expiracion < DateTime.UtcNow) return Result<TokenResponseDto>.BadRequest(default, "Refresh token no valido.");
            var usuario = _repoUsuario.DetalleUsuario(tokenInfo.idUsuario);
            var tokens = _tokenService.GenerarTokens(usuario);
            _repoToken.RevocarRefreshToken(model.refreshToken);
            _repoToken.AltaRefreshToken(usuario.idUsuario, tokens.refreshToken, tokens.emitido, DateTime.UtcNow.AddDays(7));
            return Result<TokenResponseDto>.Ok(tokens);
        }
        catch (MySqlException)
        {
            return Result<TokenResponseDto>.Unauthorized();
        }
    }

    public Result<TokenResponseDto> Logout()
    {
        try
        {
            var User = _httpContextAccessor.HttpContext?.User;
            var DateOfBirth = User.FindFirst(ClaimTypes.DateOfBirth)?.Value;
            var refreshTokenRequest = _repoToken.DetalleRefreshTokenXEmision(DateTime.Parse(DateOfBirth));
            if (refreshTokenRequest is null) return Result<TokenResponseDto>.BadRequest(default!, "Token no valido.");
            if (refreshTokenRequest.revocado) return Result<TokenResponseDto>.BadRequest(default!, "Este refresh token ya fue revocado.");
            _repoToken.RevocarRefreshToken(refreshTokenRequest.refreshToken);        
            return Result<TokenResponseDto>.Ok();
        }
        catch (MySqlException)
        {
            return Result<TokenResponseDto>.Unauthorized();
        }
    }

    // Helpers para password
    public static string HashPassword(string password)
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