namespace SuperProyecto.Api.Endpoints;

public static class UsuarioEndpoints
{
    public static void MapUsuarioEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/auth/roles", (IUsuarioService service) =>
        {
            var result = service.ObtenerRoles();
            return result.ToMinimalResult();
        }).WithTags("01 - Usuario");

        app.MapPost("/api/auth/register", (UsuarioDto usuarioDto, IUsuarioService service) =>
        {
            var result = service.AltaUsuario(usuarioDto);
            return result.ToMinimalResult();
        }).WithTags("01 - Usuario");
        
        app.MapPut("/api/usuarios/{id}/roles", (int id, int nuevoRol, IUsuarioService service) =>
        {
            var result = service.ActualizarRol(id, nuevoRol);
            return result.ToMinimalResult();
        }).WithTags("01 - Usuario").RequireAuthorization("Administrador");
    }
}