using FluentValidation;

using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.DTO;
using System.Data;

namespace SuperProyecto.Services.Validators;

public class SectorValidator : AbstractValidator<SectorDto>
{
    IRepoLocal _repoLocal;
    public SectorValidator(IRepoLocal repoLocal)
    {
        _repoLocal = repoLocal;

        RuleFor(s => s.idLocal)
            .NotEmpty().WithMessage("El idLocal es obligatorio.")
            .GreaterThan(0).WithMessage("El idLocal debe ser mayor a 0.")
            .Must(idLocal => _repoLocal.DetalleLocal(idLocal) is not null).WithMessage("El local referenciado no existe.");

        /* RuleFor(s => s.idFuncion)
            .NotEmpty().WithMessage("El idFuncion es obligatorio.")
            .GreaterThan(0).WithMessage("El idFuncion debe ser mayor a 0.")
            .Must(idFuncion => _repoFuncion.DetalleFuncion(idFuncion) is not null).WithMessage("La funcion referenciada no existe.")
            .Must(idFuncion =>
            {
                var funcion = _repoFuncion.DetalleFuncion(idFuncion);
                return funcion is not null && funcion.stock > 0;
            }).WithMessage("La funcion seleccionada ya no tiene stock."); */

        RuleFor(s => s.nombre)
            .NotEmpty().WithMessage("El nombre es obligatorio.")
            .MinimumLength(3).WithMessage("El nombre debe contener al menos 3 caracteres.")
            .MaximumLength(45).WithMessage("El nombre debe tener como máximo 45 caracteres.");
    }
}