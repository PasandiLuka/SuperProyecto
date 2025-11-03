using SuperProyecto.Core.DTO;
using SuperProyecto.Core.Entidades;

namespace SuperProyecto.Core.Persistencia;

public interface IRepoTarifa
{
    IEnumerable<Tarifa> GetTarifas(int idFuncion);
    Tarifa? DetalleTarifa(int idTarifa);
    void AltaTarifa(Tarifa tarifa);
    void UpdateTarifa(TarifaDto tarifa, int id);
}