namespace SuperProyecto.Core.Persistencia;

public interface IRepoLocal
{
    IEnumerable<Local> GetLocales();
    Local? DetalleLocal(int idLocal);
    void AltaLocal(Local local);
    void UpdateLocal(Local local, int id);
    void DeleteLocal (int idLocal);
}