using FluentValidation;
using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.DTO;

namespace SuperProyecto.Core.Validators;

public class EntradaValidator : AbstractValidator<EntradaDto>
{   
    IRepoEntrada _repoEntrada;
    public EntradaValidator(IRepoEntrada repoEntrada)
    {
        _repoEntrada = repoEntrada;

        RuleFor(e => e.idEntrada)
            .NotEmpty().WithMessage("El idEntrada es obligatorio.")
            .GreaterThan(0).WithMessage("El idEntrada debe ser mayor a 0.")
            .Must(idEntrada => _repoEntrada.DetalleEntrada(idEntrada) is null).WithMessage("Ya existe una entrada con ese idEntrada registrado.");
    }
}