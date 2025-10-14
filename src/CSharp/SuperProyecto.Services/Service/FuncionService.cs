using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.IServices;

namespace SuperProyecto.Services.Service;

public class FuncionService : IFuncionService
{
    readonly IRepoFuncion _repoFuncion;

    public FuncionService(IRepoFuncion repoFuncion)
    {
        _repoFuncion = repoFuncion;
    }

    public IEnumerable<Funcion> GetFunciones() => _repoFuncion.GetFunciones();

    public Funcion? DetalleFuncion(int id) => _repoFuncion.DetalleFuncion(id);

    public void UpdateFuncion(Funcion funcion, int id) => _repoFuncion.UpdateFuncion(funcion, id);

    public void AltaFuncion(Funcion funcion) => _repoFuncion.AltaFuncion(funcion);
}