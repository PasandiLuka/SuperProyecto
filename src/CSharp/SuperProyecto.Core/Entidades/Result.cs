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
    public byte[]? Bytes { get; private set; }

    private Result( bool success, EResultType resultType, T? data = default, string? message = null, IDictionary<string, string[]>? errors = null, byte[]? bytes = null)
    {
        Success = success;
        ResultType = resultType;
        Data = data;
        Message = message;
        Errors = errors;
        Bytes = bytes;
    }

    public static Result<T> Ok(T? data = default, string? message = null)
    => new(true, EResultType.Ok, data, message);
    
    public static Result<T> Created(T data, string? message = null)
        => new(true, EResultType.Created, data, message);

    public static Result<T> NotFound(string message)
        => new(false, EResultType.NotFound, default, message);

    public static Result<T> Unauthorized()
        => new(false, EResultType.Unauthorized, default, default, default);

    public static Result<T> BadRequest(IDictionary<string, string[]> errors = default, string? message = default)
        => new(false, EResultType.BadRequest, default, message, errors);
    
    public static Result<T> File(byte[]? bytes)
        => new(true, EResultType.File, default, default, default, bytes);
}