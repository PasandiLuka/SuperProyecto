using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.IServices;
using SuperProyecto.Core.DTO;

namespace SuperProyecto.Services.Service;

public class LocalService : ILocalService
{
    readonly IRepoLocal _repoLocal;
    public LocalService(IRepoLocal repoLocal)
    {
        _repoLocal = repoLocal;
    }

    public Result<IEnumerable<Local>> GetLocales() => Result<IEnumerable<Local>>.Ok(_repoLocal.GetLocales());

    public Result<Local?> DetalleLocal(int id) => Result<Local?>.Ok(_repoLocal.DetalleLocal(id));

    public Result<LocalDto> AltaLocal(LocalDto localDto)
    {
        Local local = ConvertirDtoClase(localDto);
        _repoLocal.AltaLocal(local);
        return Result<LocalDto>.Ok(localDto);
    }

    public Result<LocalDto> UpdateLocal(LocalDto localDto, int id)
    {
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