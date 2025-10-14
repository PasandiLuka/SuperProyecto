using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.IServices;

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

    public void AltaTarifa(Tarifa tarifa) => _repoTarifa.AltaTarifa(tarifa);

    public void UpdateTarifa(Tarifa tarifa, int id) => _repoTarifa.UpdateTarifa(tarifa, id);
}