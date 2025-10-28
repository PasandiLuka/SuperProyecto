using SuperProyecto.Core.Enums;
using SuperProyecto.Core.Entidades;

namespace SuperProyecto.Api.Helper;

public static class ResultExtensions
{
    public static IResult ToMinimalResult<T>(this Result<T> result)
    {
        /*
            La palabra clave this en este contexto no es la misma que en una clase normal.
            Aquí indica que el método es un método de extensión.

            Qué significa “método de extensión”
            Permite agregar métodos a una clase existente (en este caso ServiceResult<T>) sin modificar su código original.


            Gracias a esto, podés llamar al método como si fuera parte de la clase:

            var result = ServiceResult<TokensDto>.Ok(tokens);
            IResult httpResult = result.ToMinimalResult(); // <-- como si fuera un método de ServiceResult

            Sin this, no sería un método de extensión, y tendrías que llamarlo así:

            IResult httpResult = ResultExtensions.ToMinimalResult(result);
        */
        return result.ResultType switch
        {
            EResultType.Ok => Results.Ok(result.Data),
            EResultType.Created => Results.Created(string.Empty, result.Data),
            EResultType.NotFound => Results.NotFound(new { message = result.Message }),
            EResultType.Unauthorized => Results.Unauthorized(),
            EResultType.BadRequest => Results.BadRequest(new { errors = result.Errors, message = result.Message })
        };
    }
}