using FluentValidation;

namespace SuperProyecto.Core.Services.Validators;

public class OrdenValidator : AbstractValidator<Orden>
{
    public OrdenValidator()
    {
        RuleFor(o => o.numeroOrden)
            .NotEmpty().WithMessage("El numero de orden es obligatorio.")
            .GreaterThan(0).WithMessage("El numero de orden debe ser mayor a 0.");

        RuleFor(o => o.DNI)
            .NotEmpty().WithMessage("El DNI es obligatorio.")
            .GreaterThan(0).WithMessage("El DNI debe ser mayor a 0.");

        RuleFor(o => o.fechaCompra)
            .NotEmpty().WithMessage("La fecha hora es obligatoria.")
            .Must(date => date <= DateTime.Today).WithMessage("La fecha de compra debe ser igual o anterior a la fecha actual.");

        RuleFor(o => o.precioTotal)
            .NotEmpty().WithMessage("El precio total es obligatorio.")
            .GreaterThanOrEqualTo(0).WithMessage("El precio total debe ser mayor o igual a 0.");
    }
}