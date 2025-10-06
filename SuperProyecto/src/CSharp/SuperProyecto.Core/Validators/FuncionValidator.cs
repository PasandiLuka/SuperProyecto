using SuperProyecto.Core.DTO;
using FluentValidation;
using SuperProyecto.Core.Persistencia;

namespace SuperProyecto.Core.Validators;

public class FuncionValidator : AbstractValidator<FuncionDto>
{
    IRepoEvento _repoEvento;
    IRepoSector _repoSector;

    public FuncionValidator(IRepoEvento repoEvento, IRepoSector repoSector)
    {
        _repoEvento = repoEvento;
        _repoSector = repoSector;

        RuleFor(f => f.idEvento)
            .NotEmpty().WithMessage("El idEvento es obligatorio.")
            .GreaterThan(0).WithMessage("El idEvento debe ser mayor a 0.")
            .Must(idEvento => _repoEvento.DetalleEvento(idEvento) is not null).WithMessage("El evento referenciado no existe.");

        RuleFor(f => f.idSector)
            .NotEmpty().WithMessage("El idSector es obligatorio.")
            .GreaterThan(0).WithMessage("El idSector debe ser mayor a 0.")
            .Must(idSector => _repoSector.DetalleSector(idSector) is not null).WithMessage("El sector referenciado no existe.");

        RuleFor(f => f.fechaHora)
            .NotEmpty().WithMessage("La fecha es obligatoria.")
            .Must(date => date > DateTime.Today).WithMessage("La fecha de la funcion debe ser posterior a la fecha actual.");
    }
}