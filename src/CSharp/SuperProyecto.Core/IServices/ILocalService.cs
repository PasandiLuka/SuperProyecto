using SuperProyecto.Core.Entidades;

namespace SuperProyecto.Core.IServices;

public interface ILocalService
{
    IEnumerable<Local> GetLocales();
    Local? DetalleLocal(int id);
    void UpdateLocal(Local local, int id);
    void AltaLocal(Local local);
    void DeleteLocal(int id);
}