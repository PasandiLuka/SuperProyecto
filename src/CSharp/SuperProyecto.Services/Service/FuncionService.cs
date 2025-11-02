using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.IServices;
using SuperProyecto.Core.DTO;
using SuperProyecto.Services.Validators;
using MySqlConnector;

namespace SuperProyecto.Services.Service;

public class FuncionService : IFuncionService
{
    readonly IRepoFuncion _repoFuncion;
    readonly FuncionValidator _validador;

    public FuncionService(IRepoFuncion repoFuncion, FuncionValidator validador)
    {
        _repoFuncion = repoFuncion;
        _validador = validador;
    }

    public Result<IEnumerable<Funcion>> GetFunciones()
    {
        try
        {
            return Result<IEnumerable<Funcion>>.Ok(_repoFuncion.GetFunciones());  
        }
        catch (MySqlException)
        {
            return Result<IEnumerable<Funcion>>.Unauthorized();
        }
    }

    public Result<Funcion?> DetalleFuncion(int id)
    {
        try
        {
            var funcion = _repoFuncion.DetalleFuncion(id);
            if (funcion is null) return Result<Funcion?>.NotFound("La función solicitada no fue encontrada.");
            return Result<Funcion>.Ok(funcion);
        }
        catch (MySqlException)
        {
            return Result<Funcion>.Unauthorized();
        }
    }

    public Result<FuncionDto> UpdateFuncion(FuncionDto funcionDto, int id)
    {
        try
        {
            var resultado = _validador.Validate(funcionDto);
            if (!resultado.IsValid)
            {
                var listaErrores = resultado.Errors
                    .GroupBy(a => a.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    );
                return Result<FuncionDto>.BadRequest(listaErrores);
            }
            if(_repoFuncion.DetalleFuncion(id) is null) return Result<FuncionDto>.NotFound("La función solicitada no fue encontrada.");
            Funcion funcion = ConvertirDtoClase(funcionDto);
            _repoFuncion.UpdateFuncion(funcion, id);
            return Result<FuncionDto>.Ok(funcionDto);     
        }
        catch (MySqlException)
        {
            return Result<FuncionDto>.Unauthorized();
        }
    } 

    public Result<FuncionDto> AltaFuncion(FuncionDto funcionDto)
    {
        try
        {
        var resultado = _validador.Validate(funcionDto);
        if (!resultado.IsValid)
        {
            var listaErrores = resultado.Errors
                .GroupBy(a => a.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );
            return Result<FuncionDto>.BadRequest(listaErrores);
        }
        Funcion funcion = ConvertirDtoClase(funcionDto);
        _repoFuncion.AltaFuncion(funcion);
        return Result<FuncionDto>.Ok(funcionDto);  
        }
        catch (MySqlException)
        {
            return Result<FuncionDto>.Unauthorized();
        }
    }

    static Funcion ConvertirDtoClase(FuncionDto funcionDto)
    {
        return new Funcion
        {
            idEvento = funcionDto.idEvento,
            idLocal = funcionDto.idLocal,
            fechaHora = funcionDto.fechaHora,
            cancelada = false
        };
    }
}