using FluentValidation;

using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.DTO;
using System.Data;

namespace SuperProyecto.Services.Validators;

public class EntradaValidator : AbstractValidator<EntradaDto>
{
    IRepoTarifa _repoTarifa;
    IRepoOrden _repoOrden;
    public EntradaValidator(IRepoTarifa repoTarifa, IRepoOrden repoOrden)
    {
        _repoTarifa = repoTarifa;
        _repoOrden = repoOrden;

        RuleFor(e => e.idOrden)
            .NotEmpty().WithMessage("El idOrden es obligatorio.")
            .GreaterThan(0).WithMessage("El idOrden debe ser mayor a 0.")
            .Must(idOrden => _repoOrden.DetalleOrden(idOrden) is not null).WithMessage("La orden referenciada no existe.");
        
        RuleFor(e => e.idTarifa)
            .NotEmpty().WithMessage("El idOrden es obligatorio.")
            .GreaterThan(0).WithMessage("El idOrden debe ser mayor a 0.")
            .Must(idTarifa => _repoTarifa.DetalleTarifa(idTarifa) is not null).WithMessage("La tarifa referenciada no existe.");
    }
}