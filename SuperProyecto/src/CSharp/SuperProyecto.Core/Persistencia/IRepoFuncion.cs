namespace SuperProyecto.Core.Persistencia;


public interface IRepoFuncion
{
    IEnumerable<Funcion> GetFunciones();
    void AltaFuncion(Funcion funcion);
    Funcion? DetalleFuncion(int idFuncion);
}