using FluentValidation;

using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.DTO;

namespace SuperProyecto.Services.Validators;

public class TarifaValidator : AbstractValidator<TarifaDto>
{
    readonly IRepoFuncion _repoFuncion;
    readonly IRepoSector _repoSector;
    public TarifaValidator(IRepoFuncion repoFuncion, IRepoSector repoSector)
    {
        _repoFuncion = repoFuncion;
        _repoSector = repoSector;

        RuleFor(t => t.idFuncion)
            .NotEmpty().WithMessage("El idLocal es obligatorio.")
            .GreaterThan(0).WithMessage("El idLocal debe ser mayor a 0.")
            .Must(idFuncion => _repoFuncion.DetalleFuncion(idFuncion) is not null).WithMessage("La función referenciada no existe.");

        RuleFor(t => t.idSector)
            .NotEmpty().WithMessage("El idLocal es obligatorio.")
            .GreaterThan(0).WithMessage("El idLocal debe ser mayor a 0.")
            .Must(idSector => _repoSector.DetalleSector(idSector) is not null).WithMessage("El sector referenciado no existe.");

        RuleFor(t => t.precio)
            .NotEmpty().WithMessage("El precio es obligatorio.")
            .GreaterThanOrEqualTo(0).WithMessage("El precio debe ser mayor o igual a 0");

        RuleFor(t => t.stock)
            .NotEmpty().WithMessage("El stock es obligatorio.")
            .GreaterThanOrEqualTo(0).WithMessage("El stock debe ser mayor o igual a 0");
    }
}