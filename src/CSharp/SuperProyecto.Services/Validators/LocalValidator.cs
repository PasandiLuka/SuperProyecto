using FluentValidation;

using SuperProyecto.Core.DTO;

namespace SuperProyecto.Services.Validators;

public class LocalValidator : AbstractValidator<LocalDto>
{
    private static readonly int _cantMaximaLetrasNombre = 45;
    private static readonly int _cantMaximaLetrasParaDescripcion = 45;
    public LocalValidator()
    {
        RuleFor(l => l.nombre)
            .NotEmpty().WithMessage("El nombre es obligatorio.")
            .MinimumLength(3).WithMessage("El nombre debe contener al menos 3 caracteres.")
            .MaximumLength(_cantMaximaLetrasNombre).
                WithMessage($"El nombre debe tener menos de {_cantMaximaLetrasNombre + 1} caracteres.");

        RuleFor(l => l.direccion)
            .NotEmpty().WithMessage("La descripcion es obligatoria.")
            .MinimumLength(3).WithMessage("La direccion debe contener al menos 3 caracteres.")
            .MaximumLength(_cantMaximaLetrasParaDescripcion).
                WithMessage($"La direccion debe tener menos de {_cantMaximaLetrasParaDescripcion + 1} caracteres.");
    }
}