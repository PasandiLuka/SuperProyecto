using FluentValidation;

using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.DTO;

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
            .Must(idOrden => _repoOrden.DetalleOrden(idOrden) is not null).WithMessage("La orden referenciada no existe.")
            .Must(idOrden => _repoOrden.DetalleOrden(idOrden).cancelada).WithMessage("La orden referenciada se encuentra cancelada.")
            .Must(idOrden => _repoOrden.DetalleOrden(idOrden).pagada).WithMessage("La orden referenciada ya se encuentra pagada.");

        RuleFor(e => e.idTarifa)
            .NotEmpty().WithMessage("El idOrden es obligatorio.")
            .GreaterThan(0).WithMessage("El idOrden debe ser mayor a 0.")
            .Must(idTarifa => _repoTarifa.DetalleTarifa(idTarifa) is not null).WithMessage("La tarifa referenciada no existe.");
    }
}