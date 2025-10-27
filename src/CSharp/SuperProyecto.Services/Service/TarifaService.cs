using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.IServices;
using SuperProyecto.Core.DTO;

namespace SuperProyecto.Services.Service;

public class TarifaService : ITarifaService
{
    readonly IRepoTarifa _repoTarifa;
    public TarifaService(IRepoTarifa repoTarifa)
    {
        _repoTarifa = repoTarifa;
    }

    public Result<IEnumerable<Tarifa>> GetTarifas() => Result<IEnumerable<Tarifa>>.Ok(_repoTarifa.GetTarifas());

    public Result<Tarifa?> DetalleTarifa(int id)
    {
        if (_repoTarifa.DetalleTarifa(id) is null) return Result<Tarifa?>.NotFound("La tarifa solicitada no fue encontrada.");
        return Result<Tarifa?>.Ok(_repoTarifa.DetalleTarifa(id));
    }

    public Result<TarifaDto> AltaTarifa(TarifaDto tarifaDto)
    {
        var tarifa = ConvertirDtoClase(tarifaDto);
        _repoTarifa.AltaTarifa(tarifa);
        return Result<TarifaDto>.Ok(tarifaDto);
    }

    public Result<TarifaDto> UpdateTarifa(TarifaDto tarifaDto, int id)
    {
        var tarifa = ConvertirDtoClase(tarifaDto);
        _repoTarifa.UpdateTarifa(tarifa, id);
        return Result<TarifaDto>.Ok(tarifaDto);
    }


    static Tarifa ConvertirDtoClase(TarifaDto tarifaDto)
    {
        return new Tarifa
        {
            idSector = tarifaDto.idSector,
            precio = tarifaDto.precio
        };
    }
}