using SuperProyecto.Core.Entidades;

namespace SuperProyecto.Core.IServices;

public interface IEntradaService
{
    Result <IEnumerable<Entrada>> GetEntradas();
    Result <Entrada> DetalleEntrada(int id);
    Result <byte[]> GetQr(int id);
    Result <string> ValidarQr(int id);

}