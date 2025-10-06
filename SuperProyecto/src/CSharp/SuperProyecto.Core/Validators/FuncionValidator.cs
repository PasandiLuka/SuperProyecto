using SuperProyecto.Core.DTO;
using FluentValidation;
using SuperProyecto.Core.Persistencia;

namespace SuperProyecto.Core.Validators;

public class FuncionValidator : AbstractValidator<FuncionDto>
{
    IRepoTarifa _repoTarifa;
    IRepoEvento _repoEvento;
    public FuncionValidator(IRepoTarifa repoTarifa, IRepoEvento repoEvento)
    {
        _repoTarifa = repoTarifa;
        _repoEvento = repoEvento;

        RuleFor(f => f.idEvento)
            .NotEmpty().WithMessage("El idEvento es obligatorio.")
            .GreaterThan(0).WithMessage("El idEvento debe ser mayor a 0.")
            .Must(idEvento => _repoEvento.DetalleEvento(idEvento) is not null).WithMessage("El evento referenciado no existe.")
            .Must(idEvento => !(_repoEvento.DetalleEvento(idEvento).cancelado)).WithMessage("No se pueden agregar funciones a un evento cancelado.");

        RuleFor(f => f.idTarifa)
            .NotEmpty().WithMessage("El idTarifa es obligatorio.")
            .GreaterThan(0).WithMessage("El idTarifa debe ser mayor a 0.")
            .Must(idTarifa => _repoTarifa.DetalleTarifa(idTarifa) is not null).WithMessage("La tarifa referenciada no existe.");

        RuleFor(f => f.fechaHora)
            .NotEmpty().WithMessage("La fecha es obligatoria.")
            .Must(date => date > DateTime.Today).WithMessage("La fecha de la funcion debe ser posterior a la fecha actual.");

        RuleFor(f => f.stock)
            .NotEmpty().WithMessage("El stock es obligatorio.")
            .GreaterThanOrEqualTo(0).WithMessage("El stock debe ser mayor o igual a 0.");
    }
}