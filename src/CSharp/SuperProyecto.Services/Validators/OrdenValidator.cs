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

        RuleFor(o => o.descuento)
            .GreaterThan(0).WithMessage("El descuento no puede ser menor a 0")
            .LessThan(100).WithMessage("EL descuento no puede exceder el 100%");
    }
}