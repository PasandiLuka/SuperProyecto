using FluentValidation;

using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.DTO;

namespace SuperProyecto.Services.Validators;

public class TarifaValidator : AbstractValidator<TarifaDto>
{
    public TarifaValidator()
    {
        RuleFor(t => t.precio)
            .NotEmpty().WithMessage("El precio es obligatorio.")
            .GreaterThanOrEqualTo(0).WithMessage("El precio debe ser mayor o igual a 0");
    }
}