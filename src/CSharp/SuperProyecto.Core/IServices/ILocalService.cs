using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.DTO;

namespace SuperProyecto.Core.IServices;

public interface ILocalService
{
    IEnumerable<Local> GetLocales();
    Local? DetalleLocal(int id);
    void UpdateLocal(LocalDto local, int id);
    void AltaLocal(LocalDto local);
    void DeleteLocal(int id);
}