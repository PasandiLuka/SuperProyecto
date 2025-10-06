using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

using SuperProyecto.Core.Service;
using SuperProyecto.Core.Enums;
using SuperProyecto.Core.DTO;
using System.ComponentModel;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IRepoUsuario _repoUsuario;
    private readonly IRepoToken _repoToken;
    private readonly TokenService _tokenService;

    public AuthController(IRepoUsuario repoUsuario, IRepoToken repoToken, TokenService tokenService)
    {
        _repoUsuario = repoUsuario;
        _repoToken = repoToken;
        _tokenService = tokenService;
    }

    // GET /auth/roles
    [HttpGet("roles")]
    public IActionResult Roles()
    {
        var roles = Enum.GetNames(typeof(ERol));
        return Ok(roles);
    }


    // POST /auth/register
    [HttpPost("register")]
    public IActionResult Register([FromBody] UsuarioDto model)
    {
        var existente = _repoUsuario.DetalleUsuarioXEmail(model.email);
        if (existente != null)
            return BadRequest("El email ya está registrado.");

        var nuevoUsuario = new Usuario
        {
            email = model.email,
            passwordHash = HashPassword(model.password),
            rol = model.Rol
        };

        _repoUsuario.AltaUsuario(nuevoUsuario);
        return Created();
    }


    // POST /auth/login
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest model)
    {
        var usuario = _repoUsuario.DetalleUsuarioXEmail(model.email);
        if (usuario == null) return Unauthorized();

        if (!VerificarPassword(model.password, usuario.passwordHash))
            return Unauthorized();

        var tokens = _tokenService.GenerarTokens(usuario);

        _repoToken.AltaRefreshToken(usuario.idUsuario, tokens.refreshToken, DateTime.UtcNow.AddDays(7));

        return Ok(tokens);
    }


    // GET /auth/me
    [Authorize]
    [HttpGet("me")]
    public IActionResult Me()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null) return Unauthorized();
        var usuario = _repoUsuario.DetalleUsuario(int.Parse(userIdClaim));
        if (usuario == null) return NotFound("Usuario no encontrado.");
        return Ok(new
        {
            usuario.email,
            rol = usuario.rol.ToString()
        });
    }


    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken(RefreshTokenRequest model)
    {
        var tokenInfo = _repoToken.DetalleRefreshToken(model.refreshToken);
        if (tokenInfo == null || tokenInfo.revocado || tokenInfo.expiracion < DateTime.UtcNow)
            return Unauthorized();
        var usuario = _repoUsuario.DetalleUsuario(tokenInfo.idUsuario);
        if (usuario == null) return Unauthorized();
        var tokens = _tokenService.GenerarTokens(usuario);
        _repoToken.RevocarRefreshToken(model.refreshToken);
        _repoToken.AltaRefreshToken(usuario.idUsuario, tokens.refreshToken, DateTime.UtcNow.AddDays(7));
        return Ok(tokens);
    }


    // POST /usuarios/{id}/roles?nuevoRol=Administrador
    [Authorize(Roles = "Administrador")]
    [HttpPost("/usuarios/{id}/roles")]
    public async Task<IActionResult> CambiarRol(int id, [FromQuery] ERol nuevoRol)
    {
        var usuario = _repoUsuario.DetalleUsuario(id);
        if (usuario == null) return NotFound();
        _repoUsuario.ActualizarRol(id, nuevoRol);
        return Ok(nuevoRol);
    }


    // Helpers para password
    private string HashPassword(string password)
    {
        using var sha = SHA256.Create();
        var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
        return BitConverter.ToString(hash).Replace("-", "").ToLower();
    }

    private bool VerificarPassword(string password, string hash)
    {
        return HashPassword(password) == hash;
    }
}