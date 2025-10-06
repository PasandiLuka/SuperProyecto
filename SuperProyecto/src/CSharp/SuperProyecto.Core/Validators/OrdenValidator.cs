using SuperProyecto.Core.DTO;
using FluentValidation;
using SuperProyecto.Core.Persistencia;

namespace SuperProyecto.Core.Validators;

public class OrdenValidator : AbstractValidator<OrdenDto>
{
    IRepoCliente _repoCliente;

    public OrdenValidator(IRepoCliente repoCliente)
    {
        _repoCliente = repoCliente;

        RuleFor(o => o.DNI)
            .NotEmpty().WithMessage("El DNI es obligatorio.")
            .GreaterThan(0).WithMessage("El DNI debe ser mayor a 0.")
            .Must(DNI => _repoCliente.DetalleCliente(DNI) is not null).WithMessage("El cliente referenciado no existe.");

        RuleFor(o => o.total)
            .NotEmpty().WithMessage("El total es obligatorio.")
            .GreaterThanOrEqualTo(0).WithMessage("El total debe ser mayor o igual a 0.");
    }
}