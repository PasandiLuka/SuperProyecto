using FluentValidation;
using SuperProyecto.Core.DTO;

namespace SuperProyecto.Core.Services.Validators;

public class EntradaValidator : AbstractValidator<Entrada>
{
    public EntradaValidator()
    {        
        RuleFor(e => e.idFuncion)
            .NotEmpty().WithMessage("El idFuncion es obligatorio.")
            .GreaterThan(0).WithMessage("El idTarifa debe ser mayor a 0.");

        RuleFor(e => e.idOrden)
            .NotEmpty().WithMessage("El idOrden es obligatorio.")
            .GreaterThan(0).WithMessage("El idOrden debe ser mayor a 0.");
    }
}
