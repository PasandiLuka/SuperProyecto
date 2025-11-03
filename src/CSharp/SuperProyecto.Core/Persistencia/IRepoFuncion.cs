using SuperProyecto.Core.Entidades;

namespace SuperProyecto.Core.Persistencia;

public interface IRepoFuncion
{
    IEnumerable<Funcion> GetFunciones();
    Funcion? DetalleFuncion(int idFuncion);
    void AltaFuncion(Funcion funcion);
    void UpdateFuncion(Funcion funcion, int id);
    void CancelarFuncion(int id);
}