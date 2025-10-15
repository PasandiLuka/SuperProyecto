using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.IServices;
using SuperProyecto.Core.DTO;

namespace SuperProyecto.Services.Service;

public class LocalService : ILocalService
{
    readonly IRepoLocal _repoLocal;
    public LocalService(IRepoLocal repoLocal)
    {
        _repoLocal = repoLocal;
    }

    public IEnumerable<Local> GetLocales() => _repoLocal.GetLocales();

    public Local? DetalleLocal(int id) => _repoLocal.DetalleLocal(id);

    public void AltaLocal(LocalDto localDto)
    {
        Local local = ConvertirDtoClase(localDto);
        _repoLocal.AltaLocal(local);    
    }

    public void UpdateLocal(LocalDto localDto, int id)
    {
        Local local = ConvertirDtoClase(localDto);
        _repoLocal.UpdateLocal(local, id);
    }

    public void DeleteLocal(int id) => _repoLocal.DeleteLocal(id);

    static Local ConvertirDtoClase(LocalDto funcionDto)
    {
        return new Local
        {
            nombre = funcionDto.nombre,
            direccion = funcionDto.direccion
        };
    }
}