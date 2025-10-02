using FluentValidation;

namespace SuperProyecto.Core.Services.Validators;

public class EventoValidator : AbstractValidator<Evento>
{
    public EventoValidator()
    {      
        RuleFor(e => e.idLocal)
            .NotEmpty().WithMessage("El idLocal es obligatorio.")
            .GreaterThan(0).WithMessage("El idLocal debe ser mayor a 0.");
        
        RuleFor(e => e.fechaInicio)
            .NotEmpty().WithMessage("La fecha de inicio es obligatoria.")
            .Must(date => date > DateOnly.FromDateTime(DateTime.Today)).WithMessage("La fecha de inicio del evento debe ser posterior a la fecha actual.");
        
        RuleFor(e => e.descripcion)
            .NotEmpty().WithMessage("La descripcion es obligatoria.")
            .MinimumLength(5).WithMessage("La descripcion debe contener al menos 5 caracteres.");
    }
}