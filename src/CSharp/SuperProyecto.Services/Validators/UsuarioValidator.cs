using FluentValidation;

using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.DTO;
using SuperProyecto.Core.Enums;

namespace SuperProyecto.Services.Validators;

public class UsuarioValidator : AbstractValidator<UsuarioDto>
{   
    IRepoUsuario _repoUsuario;
    public UsuarioValidator(IRepoUsuario repoUsuario)
    {
        _repoUsuario = repoUsuario;

        RuleFor(u => u.email)
            .Must(email => !_repoUsuario.UniqueEmail(email)).WithMessage("Ya existe un usuario con ese mail registrado.")
            .NotEmpty().WithMessage("El email es obligatorio.")
            .EmailAddress().WithMessage("El email no se encuentra en un formato valido.")
            .MinimumLength(5).WithMessage("El email debe tener al menos 5 caracteres.")
            .MaximumLength(45).WithMessage("El email debe tener como máximo 45 caracteres.");

        RuleFor(u => u.password)
            .NotEmpty().WithMessage("La contrasena es obligatoria.")
            .MinimumLength(6).WithMessage("La contrasena debe contener al menos 6 caracteres.")
            .MaximumLength(45).WithMessage("La contrasena debe tener como máximo 45 caracteres.");

        RuleFor(u => u.Rol)
            .NotEmpty().WithMessage("El rol es obligatorio.")
            .Must(rol => (rol == ERol.Cliente) || (rol == ERol.Organizador)).WithMessage("El rol dado no se encuentra dentro de las opciones.");
    }
}