using SuperProyecto.Core.DTO;
using FluentValidation;
using SuperProyecto.Core.Persistencia;

namespace SuperProyecto.Core.Validators;

public class SectorValidator : AbstractValidator<SectorDto>
{
    IRepoLocal _repoLocal;
    public SectorValidator(IRepoLocal repoLocal)
    {
        _repoLocal = repoLocal;

        RuleFor(s => s.idLocal)
            .NotEmpty().WithMessage("El idLocal es obligatorio.")
            .GreaterThan(0).WithMessage("El idLocal debe ser mayor a 0.")
            .Must(idLocal => _repoLocal.DetalleLocal(idLocal) is not null).WithMessage("El local referenciado no existe.");

        RuleFor(s => s.nombre)
            .NotEmpty().WithMessage("El nombre es obligatorio.")
            .MinimumLength(3).WithMessage("El nombre debe contener al menos 3 caracteres.");
    }
}