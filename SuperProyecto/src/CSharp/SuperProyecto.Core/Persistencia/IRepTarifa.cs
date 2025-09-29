namespace SuperProyecto.Core.Persistencia;

public interface IRepoTarifa
{
    IEnumerable<Tarifa> GetTarifa();
    void AltaTarifa(Tarifa tarifa );
    Tarifa? DetalleTarifa(int idTarifa);
}