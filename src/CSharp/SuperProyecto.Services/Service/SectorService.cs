using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.IServices;
using SuperProyecto.Core.DTO;
using SuperProyecto.Services.Validators;

namespace SuperProyecto.Services.Service;

public class SectorService : ISectorService
{
    readonly IRepoSector _repoSector;
    readonly SectorValidator _validador;

    public SectorService(IRepoSector repoSector, SectorValidator validador)
    {
        _repoSector = repoSector;
        _validador = validador;
    }

    public Result<IEnumerable<Sector>> GetSectores() => Result<IEnumerable<Sector>>.Ok(_repoSector.GetSectores());

    public Result<Sector?> DetalleSector(int id)
    {
        if(_repoSector.DetalleSector(id) is null) return Result<Sector?>.NotFound("El sector solicitado no fue encontrado.");
        return Result<Sector?>.Ok(_repoSector.DetalleSector(id));
    }

    public Result<SectorDto> AltaSector(SectorDto sectorDto)
    {
        var resultado = _validador.Validate(sectorDto);
        if (!resultado.IsValid)
        {
            var listaErrores = resultado.Errors
                .GroupBy(a => a.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );
            return Result<SectorDto>.BadRequest(listaErrores);
        }
        var sector = ConvertirDtoClase(sectorDto);
        _repoSector.AltaSector(sector);
        return Result<SectorDto>.Ok(sectorDto);
    }

    public Result<SectorDto> UpdateSector(SectorDto sectorDto, int id)
    {
        var resultado = _validador.Validate(sectorDto);
        if (!resultado.IsValid)
        {
            var listaErrores = resultado.Errors
                .GroupBy(a => a.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );
            return Result<SectorDto>.BadRequest(listaErrores);
        }
        var sector = ConvertirDtoClase(sectorDto);
        _repoSector.UpdateSector(sector, id);
        return Result<SectorDto>.Ok(sectorDto);
    }

    public Result<SectorDto> DeleteSector(int id)
    {
        _repoSector.DeleteSector(id);
        return Result<SectorDto>.Ok();
    }
    
    static Sector ConvertirDtoClase(SectorDto sectorDto)
    {
        return new Sector
        {
            idLocal = sectorDto.idLocal,
            nombre = sectorDto.nombre
        };
    }
}