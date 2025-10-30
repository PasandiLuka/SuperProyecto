using FluentValidation;

using SuperProyecto.Core.DTO;

namespace SuperProyecto.Services.Validators;

public class EventoValidator : AbstractValidator<EventoDto>
{
    private static readonly int _cantMaximaLetrasNombre = 45;
    private static readonly int _cantMaximaLetrasParaDescripcion = 45;
    public EventoValidator()
    {
        RuleFor(e => e.nombre)
            .NotEmpty().WithMessage("El nombre es obligatorio.")
            .MinimumLength(3).WithMessage("El nombre debe contener al menos 3 caracteres.")
            .MaximumLength(_cantMaximaLetrasNombre).
                WithMessage($"El nombre debe tener menos de {_cantMaximaLetrasNombre + 1} caracteres.");

        RuleFor(e => e.descripcion)
            .NotEmpty().WithMessage("La descripcion es obligatoria.")
            .MinimumLength(5).WithMessage("La descripcion debe contener al menos 5 caracteres.")
            .MaximumLength(_cantMaximaLetrasParaDescripcion).
                WithMessage($"La descripcion debe tener menos de {_cantMaximaLetrasParaDescripcion + 1} caracteres.");

    }
}
