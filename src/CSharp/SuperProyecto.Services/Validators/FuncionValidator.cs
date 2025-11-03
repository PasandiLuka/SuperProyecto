using FluentValidation;

using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.DTO;

namespace SuperProyecto.Services.Validators;

public class FuncionValidator : AbstractValidator<FuncionDto>
{
    IRepoEvento _repoEvento;
    IRepoLocal _repoLocal;
    public FuncionValidator(IRepoEvento repoEvento, IRepoLocal repoLocal)
    {
        _repoEvento = repoEvento;
        _repoLocal = repoLocal;

        RuleFor(f => f.idEvento)
            .NotEmpty().WithMessage("El idEvento es obligatorio.")
            .GreaterThan(0).WithMessage("El idEvento debe ser mayor a 0.")
            .Must(idEvento => _repoEvento.DetalleEvento(idEvento) is not null).WithMessage("El evento referenciado no existe.")
            .Must(idEvento => _repoEvento.DetalleEvento(idEvento)?.publicado == true).WithMessage("No se puede agregar una funcion a un evento no publicado.")
            .Must(idEvento => _repoEvento.DetalleEvento(idEvento)?.cancelado != true).WithMessage("No se pueden agregar funciones a un evento cancelado.");

        RuleFor(f => f.idLocal)
            .NotEmpty().WithMessage("El idLocal es obligatorio.")
            .GreaterThan(0).WithMessage("El idLocal debe ser mayor a 0.")
            .Must(idLocal => _repoLocal.DetalleLocal(idLocal) is not null).WithMessage("El local referenciado no existe.");

        RuleFor(f => f.fechaHora)
            .NotEmpty().WithMessage("La fecha es obligatoria.")
            .Must(date => date > DateTime.UtcNow).WithMessage("La fecha de la funcion debe ser posterior a la fecha actual.");

    }
}