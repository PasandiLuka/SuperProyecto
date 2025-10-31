using FluentValidation;

using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.DTO;
using System.Data;

namespace SuperProyecto.Services.Validators;

public class EntradaValidator : AbstractValidator<EntradaDto>
{
    IRepoEntrada _repoEntrada;
    IRepoOrden _repoOrden;
    public EntradaValidator(IRepoEntrada repoEntrada, IRepoOrden repoOrden)
    {
        _repoEntrada = repoEntrada;
        _repoOrden = repoOrden;

        /* RuleFor(e => e.idEntrada)
            .NotEmpty().WithMessage("El idEntrada es obligatorio.")
            .GreaterThan(0).WithMessage("El idEntrada debe ser mayor a 0.")
            .Must(idEntrada => _repoEntrada.DetalleEntrada(idEntrada) is null).WithMessage("Ya existe una entrada con ese id registrado."); */
        RuleFor(e => e.idOrden)
            .NotEmpty().WithMessage("El idOrden es obligatorio.")
            .GreaterThan(0).WithMessage("El idOrden debe ser mayor a 0.")
            .Must(idOrden => _repoOrden.DetalleOrden(idOrden) is not null).WithMessage("La orden referenciada no existe.");
    }
}