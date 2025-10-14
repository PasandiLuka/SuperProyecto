using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.IServices;

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

    public void AltaLocal(Local local) => _repoLocal.AltaLocal(local);

    public void UpdateLocal(Local local, int id) => _repoLocal.UpdateLocal(local, id);

    public void DeleteLocal(int id) => _repoLocal.DeleteLocal(id);
}