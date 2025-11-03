using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.DTO;

namespace SuperProyecto.Core.IServices;

public interface ITarifaService
{
    Result<IEnumerable<Tarifa>> GetTarifas(int idFuncion);
    Result<Tarifa?> DetalleTarifa(int id);
    Result<TarifaDto> UpdateTarifa(TarifaDtoAlta tarifaDto, int id);
    Result<TarifaDto> AltaTarifa(TarifaDtoAlta tarifa);
}