using SuperProyecto.Core.Enums;

namespace SuperProyecto.Core.Entidades;

//Este servicio se crea para poder "indicar" el resultado de la operacion del servicio
//ya que como implemento servicios no dispongo de la clase IActionResult.
public class Result<T>
{
    public bool Success { get; private set; }
    public T? Data { get; private set; }
    public string? Message { get; private set; }
    public EResultType ResultType { get; private set; }
    public IDictionary<string, string[]>? Errors { get; private set; }

    private Result( bool success, EResultType resultType, T? data = default, string? message = null, IDictionary<string, string[]>? errors = null)
    {
        Success = success;
        ResultType = resultType;
        Data = data;
        Message = message;
        Errors = errors;
    }

    public static Result<T> Ok(T? data = default, string? message = null)
    => new(true, EResultType.Ok, data, message);
    
    public static Result<T> Created(T data, string? message = null)
        => new(true, EResultType.Created, data, message);

    public static Result<T> NotFound(string message)
        => new(false, EResultType.NotFound, default, message);

    public static Result<T> Unauthorized(string message)
        => new(false, EResultType.Unauthorized, default, message);

    public static Result<T> BadRequest(IDictionary<string, string[]> errors, string message = "Error de validación")
        => new(false, EResultType.BadRequest, default, message, errors);
}