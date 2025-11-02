namespace SuperProyecto.Api.Endpoints;

public static class FuncionEndpoints
{
    public static void MapFuncionEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/Funcion", (IFuncionService service) =>
        {
            var result = service.GetFunciones();
            return result.ToMinimalResult();
        }).WithTags("07 - Funcion").RequireAuthorization("Cliente");
        
        app.MapGet("/api/Funcion/{id}", (int id, IFuncionService service) =>
        {
            var result = service.DetalleFuncion(id);
            return result.ToMinimalResult();
        }).WithTags("07 - Funcion").RequireAuthorization("Cliente");

        app.MapPut("/api/Funcion/{id}", (int id, FuncionDto funcionDto, IFuncionService service) =>
        {
            var result = service.UpdateFuncion(funcionDto, id);
            return result.ToMinimalResult();
        }).WithTags("07 - Funcion").RequireAuthorization("Organizador");
            
        app.MapPost("/api/Funcion", (FuncionDto funcionDto, IFuncionService service) =>
        {
            var result = service.AltaFuncion(funcionDto);
            return result.ToMinimalResult();
        }).WithTags("07 - Funcion").RequireAuthorization("Organizador");
    }
}