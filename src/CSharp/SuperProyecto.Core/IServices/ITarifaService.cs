using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.DTO;

namespace SuperProyecto.Core.IServices;

public interface ITarifaService
{
    IEnumerable<Tarifa> GetTarifas();
    Tarifa? DetalleTarifa(int id);
    void UpdateTarifa(TarifaDto tarifa, int id);
    void AltaTarifa(TarifaDto tarifa);
}