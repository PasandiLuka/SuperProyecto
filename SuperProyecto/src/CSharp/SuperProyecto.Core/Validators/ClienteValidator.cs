using System.Data;
using FluentValidation;
using SuperProyecto.Core.Persistencia;

namespace SuperProyecto.Core.Validators;

public class ClienteValidator : AbstractValidator<Cliente>
{
    IRepoUsuario _repoUsuario;
    IRepoCliente _repoCliente;
    public ClienteValidator(IRepoUsuario repoUsuario, IRepoCliente repoCliente)
    {
        _repoUsuario = repoUsuario;
        _repoCliente = repoCliente;

        RuleFor(c => c.DNI)
            .NotEmpty().WithMessage("El DNI es obligatorio.")
            .InclusiveBetween(1000000, 99999999).WithMessage("El DNI debe tener entre 7 y 8 dígitos.")
            .Must(DNI => _repoCliente.DetalleCliente(DNI) is null).WithMessage("Ya existe un cliente con ese documento registrado.");

        RuleFor(c => c.idUsuario)
            .NotEmpty().WithMessage("El idUsuario es obligatorio.")
            .GreaterThan(0).WithMessage("El idUsuario debe ser mayor a 0.")
            .Must(idUsuario => _repoUsuario.DetalleUsuario(idUsuario) is not null).WithMessage("El usuario referenciado no existe.");

        RuleFor(c => c.nombre)
            .NotEmpty().WithMessage("El nombre es obligatorio")
            .MinimumLength(3).WithMessage("El nombre debe tener al menos 3 caracteres.");

        RuleFor(c => c.apellido)
            .NotEmpty().WithMessage("El apellido es obligatorio")
            .MinimumLength(3).WithMessage("El apellido debe tener al menos 3 caracteres.");

        RuleFor(c => c.telefono)
            .NotEmpty().WithMessage("El telefono es obligatorio");
    }
}