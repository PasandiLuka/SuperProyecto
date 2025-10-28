using SuperProyecto.Core.Entidades;
using FluentValidation;
using FluentValidation.Results;
using SuperProyecto.Core.DTO;
using SuperProyecto.Core.IServices;

namespace SuperProyecto.Services.Service;

public class ValidateService
{
    public static Dictionary<string, string[]> Validate(IValidator validator, IValidationContext classDto)
    {
        var resultado = validator.Validate(classDto);
        if (!resultado.IsValid)
        {
            var listaErrores = resultado.Errors
                .GroupBy(a => a.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );
            return listaErrores;
        }
        return null;
    }
}