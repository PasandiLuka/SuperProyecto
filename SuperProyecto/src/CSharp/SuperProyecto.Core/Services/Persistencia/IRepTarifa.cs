namespace SuperProyecto.Core.Services.Persistencia;

public interface IRepoTarifa
{
    IEnumerable<Tarifa> GetTarifa();
    Tarifa? DetalleTarifa(int idTarifa);
    void AltaTarifa(Tarifa tarifa);
    void UpdateTarifa(Tarifa tarifa, int id);
}