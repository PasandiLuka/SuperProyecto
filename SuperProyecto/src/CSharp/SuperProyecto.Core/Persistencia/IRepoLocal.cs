namespace SuperProyecto.Core.Persistencia;


public interface IRepoLocal
{
    IEnumerable<Local> GetLocales();
    void AltaLocal(Local local);
   Local? DetalleLocal(int idLocal);
}