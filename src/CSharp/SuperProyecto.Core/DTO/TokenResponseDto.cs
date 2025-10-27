namespace SuperProyecto.Core.DTO;

public class TokenResponseDto
{
    public string accessToken { get; set; }      // JWT principal
    public string refreshToken { get; set; }     // Refresh token
    public DateTime expiracion { get; set; } 
}