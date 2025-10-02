using FluentValidation;

namespace SuperProyecto.Core.Services.Validators;

public class SectorValidator : AbstractValidator<Sector>
{
    public SectorValidator()
    {
        RuleFor(s => s.idSector)
            .NotEmpty().WithMessage("El idSector es obligatorio.")
            .GreaterThan(0).WithMessage("El idSector debe ser mayor a 0.");

        RuleFor(s => s.sector)
            .NotEmpty().WithMessage("El sector es obligatorio.")
            .MinimumLength(3).WithMessage("El sector debe contener al menos 3 caracteres.");
    }
}