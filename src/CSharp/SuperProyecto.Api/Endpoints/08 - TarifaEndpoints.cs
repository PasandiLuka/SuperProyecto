namespace SuperProyecto.Api.Endpoints;

public static class TarifaEndpoints
{
    public static void MapTarifaEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/funciones/{id}/tarifas", (int idFuncion, ITarifaService service) =>
        {
            var result = service.GetTarifas(idFuncion);
            return result.ToMinimalResult();
        }).WithTags("08 - Tarifa").RequireAuthorization("Cliente");

        app.MapGet("/api/Tarifa/{id}", (int id, ITarifaService service) =>
        {
            var result = service.DetalleTarifa(id);
            return result.ToMinimalResult();
        }).WithTags("08 - Tarifa").RequireAuthorization("Cliente");

        app.MapPut("/api/Tarifa/{id}", (int id, TarifaDtoAlta tarifaDto, ITarifaService service) =>
        {
            var result = service.UpdateTarifa(tarifaDto, id);
            return result.ToMinimalResult();
        }).WithTags("08 - Tarifa").RequireAuthorization("Organizador");
                
        app.MapPost("/api/tarifas", (TarifaDtoAlta tarifaDto, ITarifaService service) =>
        {
            var result = service.AltaTarifa(tarifaDto);
            return result.ToMinimalResult();
        }).WithTags("08 - Tarifa").RequireAuthorization("Organizador");
    }
}