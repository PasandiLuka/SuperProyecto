using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.DTO;

namespace SuperProyecto.Core.IServices;

public interface IFuncionService
{
    IEnumerable<Funcion> GetFunciones();
    Funcion? DetalleFuncion(int id);
    void UpdateFuncion(FuncionDto funcion, int id);
    void AltaFuncion(FuncionDto funcion);
}