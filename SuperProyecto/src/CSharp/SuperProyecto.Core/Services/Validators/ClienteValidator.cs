using FluentValidation;
using SuperProyecto.Core.DTO;

namespace SuperProyecto.Core.Services.Validators;

public class ClienteValidator : AbstractValidator<Cliente>
{
    public ClienteValidator()
    {
        RuleFor(c => c.DNI)
            .NotEmpty().WithMessage("El DNI es obligatorio.")
            .GreaterThan(0).WithMessage("El DNI debe ser mayor a 0.");

        RuleFor(c => c.nombre)
            .NotEmpty().WithMessage("El nombre es obligatorio")
            .MinimumLength(3).WithMessage("El nombre debe tener al menos 3 caracteres.");

        RuleFor(c => c.apellido)
            .NotEmpty().WithMessage("El apellido es obligatorio")
            .MinimumLength(3).WithMessage("El apellido debe tener al menos 3 caracteres.");
        
        RuleFor(c => c.telefono)
            .NotEmpty().WithMessage("El telefono es obligatorio");    
    }
}