namespace SuperProyecto.Core.Services.Persistencia;


public interface IRepoFuncion
{
    IEnumerable<Funcion> GetFunciones();
    void AltaFuncion(Funcion funcion);
    Funcion? DetalleFuncion(int idFuncion);
    void UpdateFuncion(Funcion funcion, int id);
}