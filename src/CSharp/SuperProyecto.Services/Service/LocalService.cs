using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.IServices;
using SuperProyecto.Core.DTO;
using SuperProyecto.Services.Validators;

namespace SuperProyecto.Services.Service;

public class LocalService : ILocalService
{
    readonly IRepoLocal _repoLocal;
    readonly LocalValidator _validador;

    public LocalService(IRepoLocal repoLocal, LocalValidator validador)
    {
        _repoLocal = repoLocal;
        _validador = validador;
    }

    public Result<IEnumerable<Local>> GetLocales() => Result<IEnumerable<Local>>.Ok(_repoLocal.GetLocales());

    public Result<Local?> DetalleLocal(int id) => Result<Local?>.Ok(_repoLocal.DetalleLocal(id));

    public Result<LocalDto> AltaLocal(LocalDto localDto)
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

    public Result<LocalDto> UpdateLocal(LocalDto localDto, int id)
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

    public Result<Local> DeleteLocal(int id)
    {
        _repoLocal.DeleteLocal(id);
        return Result<Local>.Ok();
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