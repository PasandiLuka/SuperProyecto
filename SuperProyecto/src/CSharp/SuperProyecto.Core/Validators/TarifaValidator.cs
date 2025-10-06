using FluentValidation;
using SuperProyecto.Core.DTO;
using SuperProyecto.Core.Persistencia;

namespace SuperProyecto.Core.Validators;

public class TarifaValidator : AbstractValidator<TarifaDto>
{
    IRepoSector _repoSector;
    public TarifaValidator(IRepoSector repoSector)
    {
        _repoSector = repoSector;

        RuleFor(t => t.idSector)
            .NotEmpty().WithMessage("El idSector es obligatorio.")
            .GreaterThan(0).WithMessage("El idSector debe ser mayor a 0.")
            .Must(idSector => _repoSector.DetalleSector(idSector) is not null).WithMessage("El sector referenciado no existe.");

        RuleFor(t => t.precio)
            .NotEmpty().WithMessage("El precio es obligatorio.")
            .GreaterThanOrEqualTo(0).WithMessage("El precio debe ser mayor o igual a 0");
    }
}