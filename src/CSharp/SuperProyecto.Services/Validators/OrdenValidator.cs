using FluentValidation;

using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.DTO;

namespace SuperProyecto.Services.Validators;

public class OrdenValidator : AbstractValidator<OrdenDto>
{
    IRepoCliente _repoCliente;
    IRepoFuncion _repoFuncion;

    public OrdenValidator(IRepoCliente repoCliente, IRepoFuncion repoFuncion)
    {
        _repoCliente = repoCliente;
        _repoFuncion = repoFuncion;

        RuleFor(o => o.DNI)
            .NotEmpty().WithMessage("El DNI es obligatorio.")
            .GreaterThan(0).WithMessage("El DNI debe ser mayor a 0.")
            .Must(DNI => _repoCliente.DetalleCliente(DNI) is not null).WithMessage("El cliente referenciado no existe.");

        RuleFor(o => o.idFuncion)
            .NotEmpty().WithMessage("El idFuncion es obligatorio.")
            .GreaterThan(0).WithMessage("El idFuncion debe ser mayor a 0.")
            .Must(idFuncion => _repoFuncion.DetalleFuncion(idFuncion) is not null).WithMessage("La función referenciada no existe.")
            .Must(idFuncion => !_repoFuncion.DetalleFuncion(idFuncion).cancelada).WithMessage("La funcion referenciada está cancelada.")
            .Must(idFuncion => _repoFuncion.DetalleFuncion(idFuncion).stock>0).WithMessage("La funcion referenciada no posee mas stock.");
    }
}