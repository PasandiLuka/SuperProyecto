using FluentValidation;
using SuperProyecto.Core.DTO;

namespace SuperProyecto.Core.Services.Validators;

public class FuncionValidator : AbstractValidator<Funcion>
{
    public FuncionValidator()
    {
        RuleFor(f => f.idEvento)
            .NotEmpty().WithMessage("El idEvento es obligatorio.")
            .GreaterThan(0).WithMessage("El idEvento debe ser mayor a 0.");

        RuleFor(f => f.fechaHora)
            .NotEmpty().WithMessage("La fecha es obligatoria.")
            .Must(date => date > DateTime.Today).WithMessage("La fecha de la funcion debe ser posterior a la fecha actual.");
    }
}