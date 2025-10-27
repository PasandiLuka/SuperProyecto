using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.DTO;

namespace SuperProyecto.Core.IServices;

public interface IFuncionService
{
    Result<IEnumerable<Funcion>> GetFunciones();
    Result<Funcion?> DetalleFuncion(int id);
    Result<FuncionDto> UpdateFuncion(FuncionDto funcion, int id);
    Result<FuncionDto> AltaFuncion(FuncionDto funcion);
}