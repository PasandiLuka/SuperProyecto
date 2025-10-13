namespace SuperProyecto.Services.DTO;

public class TokenResponse
{
    public string accessToken { get; set; }      // JWT principal
    public string refreshToken { get; set; }     // Refresh token
    public DateTime expiracion { get; set; } 
}