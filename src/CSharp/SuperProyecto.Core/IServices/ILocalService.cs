using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.DTO;

namespace SuperProyecto.Core.IServices;

public interface ILocalService
{
    Result<IEnumerable<Local>> GetLocales();
    Result<Local?> DetalleLocal(int id);
    Result<LocalDto> UpdateLocal(LocalDto local, int id);
    Result<LocalDto> AltaLocal(LocalDto local);
    Result<Local> DeleteLocal(int id);
}