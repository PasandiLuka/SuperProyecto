using FluentValidation;

using SuperProyecto.Core.DTO;

namespace SuperProyecto.Services.Validators;

public class LocalValidator : AbstractValidator<LocalDto>
{
    public LocalValidator()
    {
        RuleFor(l => l.nombre)
            .NotEmpty().WithMessage("El nombre es obligatorio.")
            .MinimumLength(3).WithMessage("El nombre debe contener al menos 3 caracteres.");

        RuleFor(l => l.direccion)
            .NotEmpty().WithMessage("La descripcion es obligatoria.")
            .MinimumLength(3).WithMessage("La direccion debe contener al menos 3 caracteres.");
    }
}