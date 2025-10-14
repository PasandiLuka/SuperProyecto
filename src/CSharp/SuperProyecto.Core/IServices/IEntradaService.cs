using SuperProyecto.Core.Entidades;

namespace SuperProyecto.Core.IServices;

public interface IEntradaService
{
    IEnumerable<Entrada> GetEntradas();
    Entrada? DetalleEntrada(int id);
    byte[]? GetQr(int id);
    string? ValidarQr(int id);
}