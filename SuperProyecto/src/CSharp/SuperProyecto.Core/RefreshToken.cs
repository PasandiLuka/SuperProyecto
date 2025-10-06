namespace SuperProyecto.Core;

public class RefreshToken
{
    public int idRefreshToken { get; set; }
    public int idUsuario { get; set; }
    public string refreshToken { get; set;}
    public string token { get; set; }
    public DateTime expiracion { get; set; }
    public bool revocado { get; set; }
}