using FluentValidation;

namespace SuperProyecto.Core.Services.Validators;

public class TarifaValidator : AbstractValidator<Tarifa>
{
    public TarifaValidator()
    {
        RuleFor(t => t.idTarifa)
            .NotEmpty().WithMessage("El idTarifa es obligatorio.")
            .GreaterThan(0).WithMessage("El idTarifa debe ser mayor a 0.");

        RuleFor(t => t.precio)
            .NotEmpty().WithMessage("El precio es obligatorio.")
            .GreaterThanOrEqualTo(0).WithMessage("El precio debe ser mayor o igual a 0");
    }
}