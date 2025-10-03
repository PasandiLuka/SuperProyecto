using FluentValidation;
using SuperProyecto.Core.DTO;

namespace SuperProyecto.Core.Services.Validators;

public class TarifaValidator : AbstractValidator<Tarifa>
{
    public TarifaValidator()
    {
        RuleFor(t => t.precio)
            .NotEmpty().WithMessage("El precio es obligatorio.")
            .GreaterThanOrEqualTo(0).WithMessage("El precio debe ser mayor o igual a 0");
    }
}