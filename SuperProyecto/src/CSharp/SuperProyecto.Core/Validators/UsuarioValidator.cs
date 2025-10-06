using SuperProyecto.Core.DTO;
using FluentValidation;
using SuperProyecto.Core.Persistencia;

namespace SuperProyecto.Core.Validators;

public class UsuarioValidator : AbstractValidator<UsuarioDto>
{   
    IRepoUsuario _repoUsuario;
    public UsuarioValidator(IRepoUsuario repoUsuario)
    {
        _repoUsuario = repoUsuario;
        
        RuleFor(u => u.email)
            .NotEmpty().WithMessage("El email es obligatorio.")
            .EmailAddress().WithMessage("El email no se encuentra en un formato valido.")
            .Must(email => _repoUsuario.UniqueEmail(email)).WithMessage("Ya existe un usuario con ese mail registrado.");

        RuleFor(u => u.password)
            .NotEmpty().WithMessage("La contrasena es obligatoria.")
            .MinimumLength(6).WithMessage("La contrasena debe contener al menos 6 caracteres.");

        RuleFor(u => u.Rol)
            .NotEmpty().WithMessage("El rol es obligatorio.")
            .IsInEnum().WithMessage("El rol dado no se encuentra dentro de las opciones.");
    }
}