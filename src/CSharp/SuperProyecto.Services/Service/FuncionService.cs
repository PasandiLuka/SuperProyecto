using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.IServices;
using SuperProyecto.Core.DTO;

namespace SuperProyecto.Services.Service;

public class FuncionService : IFuncionService
{
    readonly IRepoFuncion _repoFuncion;

    public FuncionService(IRepoFuncion repoFuncion)
    {
        _repoFuncion = repoFuncion;
    }

    public Result<IEnumerable<Funcion>> GetFunciones() => Result<IEnumerable<Funcion>>.Ok(_repoFuncion.GetFunciones());

    public Result<Funcion?> DetalleFuncion(int id)
    {
        var funcion = _repoFuncion.DetalleFuncion(id);
        if (funcion is null) return Result<Funcion?>.NotFound("La función solicitada no fue encontrada.");
        return Result<Funcion>.Ok(funcion);
    }

    public Result<FuncionDto> UpdateFuncion(FuncionDto funcionDto, int id)
    {
        if(_repoFuncion.DetalleFuncion(id) is null) return Result<FuncionDto>.NotFound("La función solicitada no fue encontrada.");
        Funcion funcion = ConvertirDtoClase(funcionDto);
        _repoFuncion.UpdateFuncion(funcion, id);
        return Result<FuncionDto>.Ok(funcionDto);
    } 

    public Result<FuncionDto> AltaFuncion(FuncionDto funcionDto)
    {
        Funcion funcion = ConvertirDtoClase(funcionDto);
        _repoFuncion.AltaFuncion(funcion);
        return Result<FuncionDto>.Ok(funcionDto);
    }

    static Funcion ConvertirDtoClase(FuncionDto funcionDto)
    {
        return new Funcion
        {
            idEvento = funcionDto.idEvento,
            idTarifa = funcionDto.idTarifa,
            fechaHora = funcionDto.fechaHora,
            stock = funcionDto.stock,
            cancelada = false
        };
    }
}