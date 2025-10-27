using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.IServices;
using SuperProyecto.Core.DTO;

namespace SuperProyecto.Services.Service;

public class SectorService : ISectorService
{
    readonly IRepoSector _repoSector;
    public SectorService(IRepoSector repoSector)
    {
        _repoSector = repoSector;
    }

    public Result<IEnumerable<Sector>> GetSectores() => Result<IEnumerable<Sector>>.Ok(_repoSector.GetSectores());

    public Result<Sector?> DetalleSector(int id)
    {
        if(_repoSector.DetalleSector(id) is null) return Result<Sector?>.NotFound("El sector solicitado no fue encontrado.");
        return Result<Sector?>.Ok(_repoSector.DetalleSector(id));
    }

    public Result<SectorDto> AltaSector(SectorDto sectorDto)
    {
        var sector = ConvertirDtoClase(sectorDto);
        _repoSector.AltaSector(sector);
        return Result<SectorDto>.Ok(sectorDto);
    }

    public Result<SectorDto> UpdateSector(SectorDto sectorDto, int id)
    {
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