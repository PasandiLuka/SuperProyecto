using FluentValidation;

namespace SuperProyecto.Core.Services.Validators;

public class FuncionValidator : AbstractValidator<Funcion>
{
    public FuncionValidator()
    {
        RuleFor(f => f.idFuncion)
            .NotEmpty().WithMessage("El idFuncion es obligatorio.")
            .GreaterThan(0).WithMessage("El idFuncion debe ser mayor a 0.");

        RuleFor(f => f.idEvento)
            .NotEmpty().WithMessage("El idEvento es obligatorio.")
            .GreaterThan(0).WithMessage("El idEvento debe ser mayor a 0.");

        RuleFor(f => f.descripcion)
            .NotEmpty().WithMessage("La descripcion es obligatoria.")
            .MinimumLength(5).WithMessage("La descripcion debe contener al menos 5 caracteres.");

        RuleFor(f => f.fechaHora)
            .NotEmpty().WithMessage("La fecha es obligatoria.")
            .Must(date => date > DateTime.Today).WithMessage("La fecha de la funcion debe ser posterior a la fecha actual.");
    }
}