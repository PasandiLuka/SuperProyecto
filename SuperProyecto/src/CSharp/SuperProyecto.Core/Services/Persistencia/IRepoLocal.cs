namespace SuperProyecto.Core.Services.Persistencia;


public interface IRepoLocal
{
    IEnumerable<Local> GetLocales();
    void AltaLocal(Local local);
    Local? DetalleLocal(int idLocal);
    void UpdateLocal(Local local, int id);
}