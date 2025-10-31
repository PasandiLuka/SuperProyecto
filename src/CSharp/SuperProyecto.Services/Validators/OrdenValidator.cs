using FluentValidation;

using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.DTO;

namespace SuperProyecto.Services.Validators;

public class OrdenValidator : AbstractValidator<OrdenDto>
{
    IRepoCliente _repoCliente;
    IRepoSector _repoSector;

    public OrdenValidator(IRepoCliente repoCliente, IRepoSector repoSector)
    {
        _repoCliente = repoCliente;
        _repoSector = repoSector;

        RuleFor(o => o.DNI)
            .NotEmpty().WithMessage("El DNI es obligatorio.")
            .GreaterThan(0).WithMessage("El DNI debe ser mayor a 0.")
            .Must(DNI => _repoCliente.DetalleCliente(DNI) is not null).WithMessage("El cliente referenciado no existe.");

        RuleFor(o => o.idSector)
            .NotEmpty().WithMessage("El idFuncion es obligatorio.")
            .GreaterThan(0).WithMessage("El idFuncion debe ser mayor a 0.")
            .Must(idSector => _repoSector.DetalleSector(idSector) is not null).WithMessage("El sector referenciado no existe.")
            .Must(idSector => _repoSector.DetalleSector(idSector).capacidad < 0).WithMessage("El sector referenciado esta al limite de su capacidad.");
    }
}