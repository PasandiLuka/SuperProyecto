using FluentValidation;
using SuperProyecto.Core.DTO;
using SuperProyecto.Core.Persistencia;

namespace SuperProyecto.Core.Validators;

public class TarifaValidator : AbstractValidator<TarifaDto>
{
    IRepoFuncion _repoFuncion;
    public TarifaValidator(IRepoFuncion repoFuncion)
    {
        _repoFuncion = repoFuncion;

        RuleFor(t => t.idFuncion)
            .NotEmpty().WithMessage("El idFuncion es obligatorio.")
            .GreaterThan(0).WithMessage("El idFuncion debe ser mayor a 0.")
            .Must(idFuncion => _repoFuncion.DetalleFuncion(idFuncion) is not null).WithMessage("La funcion referenciada no existe.");

        RuleFor(t => t.nombre)
            .NotEmpty().WithMessage("El nombre es obligatorio.")
            .MinimumLength(3).WithMessage("El nombre debe contener al menos 3 caracteres.");

        RuleFor(t => t.precio)
            .NotEmpty().WithMessage("El precio es obligatorio.")
            .GreaterThanOrEqualTo(0).WithMessage("El precio debe ser mayor o igual a 0");

        RuleFor(t => t.stock)
            .NotEmpty().WithMessage("El stock es obligatorio.")
            .GreaterThan(0).WithMessage("El stock debe ser mayor a 0.");
    }
}