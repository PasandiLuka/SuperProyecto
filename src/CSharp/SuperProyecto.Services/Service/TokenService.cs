using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.DTO;

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace SuperProyecto.Services.Service;

public class TokenService
{
    private readonly IConfiguration _config;

    public TokenService(IConfiguration config)
    {
        _config = config;
    }

    public string GenerarToken(Usuario usuario)
    {
        int expireMinutes = Convert.ToInt32(_config["Jwt:ExpireMinutes"]);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.idUsuario.ToString()),
            new Claim(ClaimTypes.Email, usuario.email),
            new Claim(ClaimTypes.Role, usuario.rol.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expireMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    // Genera un refresh token seguro
    public string GenerarRefreshToken() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    

    // Genera ambos tokens y devuelve DTO
    public TokenResponse GenerarTokens(Usuario usuario)
    {
        var accessToken = GenerarToken(usuario);
        var refreshToken = GenerarRefreshToken();
        var expiracion = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_config["Jwt:ExpireMinutes"]));

        return new TokenResponse
        {
            accessToken = accessToken,
            refreshToken = refreshToken,
            expiracion = expiracion
        };
    }
}