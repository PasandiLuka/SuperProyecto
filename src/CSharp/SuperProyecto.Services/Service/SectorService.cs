using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.IServices;
using SuperProyecto.Core.DTO;
using SuperProyecto.Services.Validators;
using MySqlConnector;

namespace SuperProyecto.Services.Service;

public class SectorService : ISectorService
{
    readonly IRepoSector _repoSector;
    readonly IRepoOrden _repoOrden;
    readonly SectorValidator _validador;

    public SectorService(IRepoSector repoSector, IRepoOrden repoOrden, SectorValidator validador)
    {
        _repoSector = repoSector;
        _repoOrden = repoOrden;
        _validador = validador;
    }

    public Result<IEnumerable<Sector>> GetSectores()
    {
        try
        {
            return Result<IEnumerable<Sector>>.Ok(_repoSector.GetSectores());
        }
        catch (MySqlException)
        {
            return Result<IEnumerable<Sector>>.Unauthorized();
        }
    }

    public Result<Sector?> DetalleSector(int id)
    {
        try
        {
            if(_repoSector.DetalleSector(id) is null) return Result<Sector?>.NotFound("El sector solicitado no fue encontrado.");
            return Result<Sector?>.Ok(_repoSector.DetalleSector(id));
        }
        catch (MySqlException)
        {
            return Result<Sector>.Unauthorized();
        }
    }

    public Result<SectorDto> AltaSector(SectorDto sectorDto)
    {
        try
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
        catch (MySqlException)
        {
            return Result<SectorDto>.Unauthorized();
        }
    }

    public Result<SectorDto> UpdateSector(SectorDto sectorDto, int id)
    {
        try
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
        catch (MySqlException)
        {
            return Result<SectorDto>.Unauthorized();
        }
    }

    public Result<SectorDto> DeleteSector(int id)
    {
        try
        {
            var orden = _repoOrden.DetalleOrdenDeleteSector(id);
            if (orden is not null) return Result<SectorDto>.BadRequest(default, "No se puede eliminar el sector indicado.");
            _repoSector.DeleteSector(id);
            return Result<SectorDto>.Ok();
        }
        catch (MySqlException)
        {
            return Result<SectorDto>.Unauthorized();
        }
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