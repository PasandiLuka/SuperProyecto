using FluentValidation;
using SuperProyecto.Core.DTO;

namespace SuperProyecto.Core.Services.Validators;

public class LocalValidator : AbstractValidator<Local>
{
    public LocalValidator()
    {
        RuleFor(l => l.direccion)
            .NotEmpty().WithMessage("La descripcion es obligatoria.")
            .MinimumLength(3).WithMessage("La direccion debe contener al menos 3 caracteres");
    }
}