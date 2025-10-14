using SuperProyecto.Core.Entidades;

namespace SuperProyecto.Core.IServices;

public interface IFuncionService
{
    IEnumerable<Funcion> GetFunciones();
    Funcion? DetalleFuncion(int id);
    void UpdateFuncion(Funcion funcion, int id);
    void AltaFuncion(Funcion funcion);
}