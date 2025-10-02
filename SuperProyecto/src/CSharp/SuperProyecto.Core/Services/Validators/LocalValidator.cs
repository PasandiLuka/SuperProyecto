using FluentValidation;

namespace SuperProyecto.Core.Services.Validators;

public class LocalValidator : AbstractValidator<Local>
{
    public LocalValidator()
    {
        RuleFor(l => l.direccion)
            .NotEmpty().WithMessage("La descripcion es obligatoria.")
            .MinimumLength(3).WithMessage("La direccion debe contener al menos 3 caracteres");
        
        RuleFor(l => l.capacidadMax)
            .NotEmpty().WithMessage("La capacidad maxima es obligatoria")
            .GreaterThan(0).WithMessage("La capacidad maxima debe ser mayor a 0");
    }
}