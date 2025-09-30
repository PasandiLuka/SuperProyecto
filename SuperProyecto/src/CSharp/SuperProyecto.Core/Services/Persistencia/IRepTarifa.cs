namespace SuperProyecto.Core.Services.Persistencia;

public interface IRepoTarifa
{
    IEnumerable<Tarifa> GetTarifa();
    void AltaTarifa(Tarifa tarifa);
    Tarifa? DetalleTarifa(int idTarifa);
    void UpdateTarifa(Tarifa tarifa, int id);
}