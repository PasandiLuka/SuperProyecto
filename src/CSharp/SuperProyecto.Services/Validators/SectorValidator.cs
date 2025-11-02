using FluentValidation;

using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.DTO;
using System.Data;

namespace SuperProyecto.Services.Validators;

public class SectorValidator : AbstractValidator<SectorDto>
{
    IRepoLocal _repoLocal;
    IRepoFuncion _repoFuncion;
    IRepoTarifa _repoTarifa;
    public SectorValidator(IRepoLocal repoLocal, IRepoFuncion repoFuncion, IRepoTarifa repoTarifa)
    {
        _repoLocal = repoLocal;
        _repoFuncion = repoFuncion;
        _repoTarifa = repoTarifa;

        RuleFor(s => s.idLocal)
            .NotEmpty().WithMessage("El idLocal es obligatorio.")
            .GreaterThan(0).WithMessage("El idLocal debe ser mayor a 0.")
            .Must(idLocal => _repoLocal.DetalleLocal(idLocal) is not null).WithMessage("El local referenciado no existe.");

        RuleFor(s => s.idFuncion)
            .NotEmpty().WithMessage("El idFuncion es obligatorio.")
            .GreaterThan(0).WithMessage("El idFuncion debe ser mayor a 0.")
            .Must(idFuncion => _repoFuncion.DetalleFuncion(idFuncion) is not null).WithMessage("La funcion referenciada no existe.")
            .Must(idFuncion =>
            {
                var funcion = _repoFuncion.DetalleFuncion(idFuncion);
                return funcion is not null && funcion.stock > 0;
            }).WithMessage("La funcion seleccionada ya no tiene stock.");

        RuleFor(s => s.idTarifa)
            .NotEmpty().WithMessage("El idTarifa es obligatorio.")
            .GreaterThan(0).WithMessage("El idTarifa debe ser mayor a 0.")
            .Must(idTarifa => _repoTarifa.DetalleTarifa(idTarifa) is not null).WithMessage("La tarifa referenciada no existe.");

        RuleFor(s => s.nombre)
            .NotEmpty().WithMessage("El nombre es obligatorio.")
            .MinimumLength(3).WithMessage("El nombre debe contener al menos 3 caracteres.")
            .MaximumLength(45).WithMessage("El nombre debe tener como máximo 45 caracteres.");

        RuleFor(s => s.capacidad)
            .NotEmpty().WithMessage("La capacidad es obligatoria.")
            .GreaterThan(0).WithMessage("La capacidad debe ser mayor a 0.")
            .LessThan(int.MaxValue).WithMessage("El numero maximo es 2.000.000.000");
    }
}