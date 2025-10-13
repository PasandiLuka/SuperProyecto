using FluentValidation;

using SuperProyecto.Services.DTO;

namespace SuperProyecto.Services.Validators;

public class EventoValidator : AbstractValidator<EventoDto>
{
    public EventoValidator()
    {
        RuleFor(e => e.nombre)
            .NotEmpty().WithMessage("El nombre es obligatorio.")
            .MinimumLength(3).WithMessage("El nombre debe contener al menos 3 caracteres.");
        
        RuleFor(e => e.descripcion)
            .NotEmpty().WithMessage("La descripcion es obligatoria.")
            .MinimumLength(5).WithMessage("La descripcion debe contener al menos 5 caracteres.");
    }
}