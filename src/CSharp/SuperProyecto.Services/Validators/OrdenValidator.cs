using FluentValidation;

using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.DTO;

namespace SuperProyecto.Services.Validators;

public class OrdenValidator : AbstractValidator<OrdenDto>
{
    IRepoCliente _repoCliente;
    public OrdenValidator(IRepoCliente repoCliente)
    {
        _repoCliente = repoCliente;

        RuleFor(o => o.idCliente)
            .NotEmpty().WithMessage("El idCliente es obligatorio.")
            .GreaterThan(0).WithMessage("El idCliente debe ser mayor a 0.")
            .Must(idCliente => _repoCliente.DetalleCliente(idCliente) is not null).WithMessage("El cliente referenciado no existe.");
    }
}