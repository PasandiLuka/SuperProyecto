using FluentValidation;
using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.DTO;

namespace SuperProyecto.Core.Validators;

public class EntradaValidator : AbstractValidator<EntradaDto>
{   
    IRepoFuncion _repoFuncion;
    IRepoOrden _repoOrden;
    IRepoEntrada _repoEntrada;
    public EntradaValidator(IRepoFuncion repoFuncion, IRepoOrden repoOrden, IRepoEntrada repoEntrada)
    {
        _repoFuncion = repoFuncion;
        _repoOrden = repoOrden;
        _repoEntrada = repoEntrada;

        RuleFor(e => e.idEntrada)
            .NotEmpty().WithMessage("El idEntrada es obligatorio.")
            .GreaterThan(0).WithMessage("El idEntrada debe ser mayor a 0.")
            .Must(idEntrada => _repoEntrada.DetalleEntrada(idEntrada) is null).WithMessage("Ya existe una entrada con ese idEntrada registrado.");

        RuleFor(e => e.idFuncion)
            .NotEmpty().WithMessage("El idFuncion es obligatorio.")
            .GreaterThan(0).WithMessage("El idTarifa debe ser mayor a 0.")
            .Must(idFuncion => _repoFuncion.DetalleFuncion(idFuncion) is not null).WithMessage("La funcion referenciada no existe.");

        RuleFor(e => e.idOrden)
            .NotEmpty().WithMessage("El idOrden es obligatorio.")
            .GreaterThan(0).WithMessage("El idOrden debe ser mayor a 0.")
            .Must(idOrden => _repoOrden.DetalleOrden(idOrden) is not null).WithMessage("La orden referenciada no existe.");
    }
}