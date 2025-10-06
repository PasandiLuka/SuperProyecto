using System.Data;

namespace SuperProyecto.Core.Persistencia;

public interface IRepoQr
{
    Qr? DetalleQr(int idQr);
    void AltaQr(int idEntrada, string url);
}