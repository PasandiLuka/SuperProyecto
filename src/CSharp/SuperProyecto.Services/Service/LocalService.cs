using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.IServices;
using SuperProyecto.Core.DTO;
using SuperProyecto.Services.Validators;
using MySqlConnector;
namespace SuperProyecto.Services.Service;

public class LocalService : ILocalService
{
    readonly IRepoLocal _repoLocal;
    readonly IRepoSector _repoSector;
    readonly LocalValidator _validador;

    public LocalService(IRepoLocal repoLocal, IRepoSector repoSector, LocalValidator validador)
    {
        _repoLocal = repoLocal;
        _repoSector = repoSector;
        _validador = validador;
    }

    public Result<IEnumerable<Local>> GetLocales()
    {
        try
        {
            return Result<IEnumerable<Local>>.Ok(_repoLocal.GetLocales());
        }
        catch (MySqlException)
        {
            return Result<IEnumerable<Local>>.Unauthorized();
        }
    }

    public Result<Local?> DetalleLocal(int id)
    {
        try
        {
            return Result<Local?>.Ok(_repoLocal.DetalleLocal(id)); 
        }
        catch (MySqlException)
        {
            return Result<Local>.Unauthorized();
        }
    }

    public Result<LocalDto> AltaLocal(LocalDto localDto)
    {
        try
        {
            var resultado = _validador.Validate(localDto);
            if (!resultado.IsValid)
            {
                var listaErrores = resultado.Errors
                    .GroupBy(a => a.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    );
                return Result<LocalDto>.BadRequest(listaErrores);
            }
            Local local = ConvertirDtoClase(localDto);
            _repoLocal.AltaLocal(local);
            return Result<LocalDto>.Ok(localDto);
        }
        catch (MySqlException)
        {
            return Result<LocalDto>.Unauthorized();
        }
    }

    public Result<LocalDto> UpdateLocal(LocalDto localDto, int id)
    {
        try
        {
            var resultado = _validador.Validate(localDto);
            if (!resultado.IsValid)
            {
                var listaErrores = resultado.Errors
                    .GroupBy(a => a.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    );
                return Result<LocalDto>.BadRequest(listaErrores);
            }
            if(_repoLocal.DetalleLocal(id) is null) return Result<LocalDto>.NotFound("El local a modificar no fue encontrado.");
            Local local = ConvertirDtoClase(localDto);
            _repoLocal.UpdateLocal(local, id);
            return Result<LocalDto>.Ok(localDto);
        }
        catch (MySqlException)
        {
            return Result<LocalDto>.Unauthorized();
        }
    }

    public Result<Local> DeleteLocal(int id)
    {
        try
        {
            var sector = _repoSector.DetalleSectorDeleteLocal(id);
            if (sector is not null) return Result<Local>.BadRequest(default, "No se puede eliminar el local seleccionado.");
            _repoLocal.DeleteLocal(id);
            return Result<Local>.Ok();
        }
        catch (MySqlException)
        {
            return Result<Local>.Unauthorized();
        }
    }

    static Local ConvertirDtoClase(LocalDto funcionDto)
    {
        return new Local
        {
            nombre = funcionDto.nombre,
            direccion = funcionDto.direccion
        };
    }
}