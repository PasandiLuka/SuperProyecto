namespace SuperProyecto.Core.Services.Persistencia;


public interface IRepoFuncion
{
    IEnumerable<Funcion> GetFunciones();
    Funcion? DetalleFuncion(int idFuncion);
    void AltaFuncion(Funcion funcion);
    void UpdateFuncion(Funcion funcion, int id);
}