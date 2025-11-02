namespace SuperProyecto.Api.Endpoints;

public static class OrdenEnpoints
{
    public static void MapOrdenEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/Orden", (IOrdenService service) =>
        {
            var result = service.GetOrdenes();
            return result.ToMinimalResult();
        }).WithTags("09 - Orden").RequireAuthorization("Cliente");
            
        app.MapGet("/api/Orden/{id}", (int id, IOrdenService service) =>
        {
            var result = service.DetalleOrden(id);
            return result.ToMinimalResult();
        }).WithTags("09 - Orden").RequireAuthorization("Cliente");

        app.MapPost("/api/Orden", (OrdenDto ordenDto, IOrdenService service) =>
        {
            var result = service.AltaOrden(ordenDto);
            return result.ToMinimalResult();
        }).WithTags("09 - Orden").RequireAuthorization("Cliente");
            
        app.MapPut("/api/Orden/{id}/pagar", (int id, IOrdenService service) =>
        {
            var result = service.PagarOrden(id);
            return result.ToMinimalResult();
        }).WithTags("09 - Orden").RequireAuthorization("Cliente");
    }
}