namespace SuperProyecto.Core.IServices;

public interface IQrService
{
    string GenerarQrUrl(int idEntrada);
    byte[] CrearQR(string url);
}