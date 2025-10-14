using SuperProyecto.Core.Entidades;

namespace SuperProyecto.Core.IServices;

public interface ITarifaService
{
    IEnumerable<Tarifa> GetTarifas();
    Tarifa? DetalleTarifa(int id);
    void UpdateTarifa(Tarifa tarifa, int id);
    void AltaTarifa(Tarifa tarifa);
}