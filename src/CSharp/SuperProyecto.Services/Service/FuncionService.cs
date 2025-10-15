using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.IServices;
using SuperProyecto.Core.DTO;

namespace SuperProyecto.Services.Service;

public class FuncionService : IFuncionService
{
    readonly IRepoFuncion _repoFuncion;

    public FuncionService(IRepoFuncion repoFuncion)
    {
        _repoFuncion = repoFuncion;
    }

    public IEnumerable<Funcion> GetFunciones() => _repoFuncion.GetFunciones();

    public Funcion? DetalleFuncion(int id) => _repoFuncion.DetalleFuncion(id);

    public void UpdateFuncion(FuncionDto funcionDto, int id)
    {
        Funcion funcion = ConvertirDtoClase(funcionDto);
        _repoFuncion.UpdateFuncion(funcion, id);  
    } 

    public void AltaFuncion(FuncionDto funcionDto)
    {
        Funcion funcion = ConvertirDtoClase(funcionDto);
        _repoFuncion.AltaFuncion(funcion);
    }

    static Funcion ConvertirDtoClase(FuncionDto funcionDto)
    {
        return new Funcion
        {
            idEvento = funcionDto.idEvento,
            idTarifa = funcionDto.idTarifa,
            fechaHora = funcionDto.fechaHora,
            stock = funcionDto.stock,
            cancelada = false
        };
    }
}