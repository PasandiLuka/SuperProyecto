namespace SuperProyecto.Api.Endpoints;

public static class TarifaEndpoints
{
    public static void MapTarifaEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/Tarifa", (ITarifaService service) =>
        {
            var result = service.GetTarifas();
            return result.ToMinimalResult();
        }).WithTags("06 - Tarifa").RequireAuthorization("Cliente");

        app.MapGet("/api/Tarifa/{id}", (int id, ITarifaService service) =>
        {
            var result = service.DetalleTarifa(id);
            return result.ToMinimalResult();
        }).WithTags("06 - Tarifa").RequireAuthorization("Cliente");

        app.MapPut("/api/Tarifa/{id}", (int id, TarifaDto tarifaDto, ITarifaService service) =>
        {
            var result = service.UpdateTarifa(tarifaDto, id);
            return result.ToMinimalResult();
        }).WithTags("06 - Tarifa").RequireAuthorization("Organizador");
                
        app.MapPost("/api/Tarifa", (TarifaDto tarifaDto, ITarifaService service) =>
        {
            var result = service.AltaTarifa(tarifaDto);
            return result.ToMinimalResult();
        }).WithTags("06 - Tarifa").RequireAuthorization("Organizador");
    }
}