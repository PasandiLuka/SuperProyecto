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

    public IEnumerable<Tarifa> GetTarifas() => _repoTarifa.GetTarifas();

    public Tarifa? DetalleTarifa(int id) => _repoTarifa.DetalleTarifa(id);

    public void AltaTarifa(TarifaDto tarifaDto)
    {
        var tarifa = ConvertirDtoClase(tarifaDto);
        _repoTarifa.AltaTarifa(tarifa);
    }

    public void UpdateTarifa(TarifaDto tarifaDto, int id)
    {
        var tarifa = ConvertirDtoClase(tarifaDto);
        _repoTarifa.UpdateTarifa(tarifa, id);
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