using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.DTO;

namespace SuperProyecto.Core.IServices;

public interface ITarifaService
{
    Result<IEnumerable<Tarifa>> GetTarifas();
    Result<Tarifa?> DetalleTarifa(int id);
    Result<TarifaDto> UpdateTarifa(TarifaDto tarifa, int id);
    Result<TarifaDto> AltaTarifa(TarifaDto tarifa);
}