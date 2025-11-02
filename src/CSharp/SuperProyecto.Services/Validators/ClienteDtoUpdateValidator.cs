using FluentValidation;

using SuperProyecto.Core.DTO;

namespace SuperProyecto.Services.Validators;

public class ClienteDtoUpdateValidator : AbstractValidator<ClienteDtoUpdate>
{
    public ClienteDtoUpdateValidator()
    {
        RuleFor(c => c.nombre)
            .NotEmpty().WithMessage("El nombre es obligatorio")
            .MinimumLength(3).WithMessage("El nombre debe tener al menos 3 caracteres.")
            .MaximumLength(45).WithMessage("El nombre debe tener como máximo 45 caracteres.");

        RuleFor(c => c.apellido)
            .NotEmpty().WithMessage("El apellido es obligatorio")
            .MinimumLength(3).WithMessage("El apellido debe tener al menos 3 caracteres.")
            .MaximumLength(45).WithMessage("El apellido debe tener como máximo 45 caracteres.");
    }
}