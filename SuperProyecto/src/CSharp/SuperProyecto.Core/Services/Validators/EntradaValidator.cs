using FluentValidation;

namespace SuperProyecto.Core.Services.Validators;

public class EntradaValidator : AbstractValidator<Entrada>
{
    public EntradaValidator()
    {
        RuleFor(e => e.idTarifa)
            .NotEmpty().WithMessage("El idTarifa es obligatorio.")
            .GreaterThan(0).WithMessage("El idTarifa debe ser mayor a 0.");
        
        RuleFor(e => e.idFuncion)
            .NotEmpty().WithMessage("El idFuncion es obligatorio.")
            .GreaterThan(0).WithMessage("El idTarifa debe ser mayor a 0.");

        RuleFor(e => e.numeroOrden)
            .NotEmpty().WithMessage("El numero de orden es obligatorio.")
            .GreaterThan(0).WithMessage("El numero de orden debe ser mayor a 0.");
    }
}
