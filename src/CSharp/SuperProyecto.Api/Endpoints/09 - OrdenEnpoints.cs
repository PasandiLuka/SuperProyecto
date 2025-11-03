using Microsoft.AspNetCore.Mvc;

namespace SuperProyecto.Api.Endpoints;

public static class OrdenEnpoints
{
    public static void MapOrdenEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/ordenes", (IOrdenService service) =>
        {
            var result = service.GetOrdenes();
            return result.ToMinimalResult();
        }).WithTags("09 - Orden").RequireAuthorization("Cliente");
            
        app.MapGet("/api/ordenes/{id}", (int id, IOrdenService service) =>
        {
            var result = service.DetalleOrden(id);
            return result.ToMinimalResult();
        }).WithTags("09 - Orden").RequireAuthorization("Cliente");

        app.MapPost("/api/ordenes", (OrdenDto ordenDto, IOrdenService service) =>
        {
            var result = service.AltaOrden(ordenDto);
            return result.ToMinimalResult();
        }).WithTags("09 - Orden").RequireAuthorization("Cliente");

        app.MapPost("/api/ordenes/{id}/pagar", (int id, IOrdenService service) =>
        {
            var result = service.PagarOrden(id);
            return result.ToMinimalResult();
        }).WithTags("09 - Orden").RequireAuthorization("Cliente");

        app.MapPost("/api/ordenes/{id}/cancelar", (int id, IOrdenService service) =>
        {
            var result = service.CancelarOrden(id);
            return result.ToMinimalResult();
        }).WithTags("09 - Orden").RequireAuthorization("Cliente");

        app.MapPost("api/ordenes/{id}/crearentrada", (int id, [FromQuery] int idTarifa, IOrdenService service) =>
        {
            var result = service.CrearEntrada(id, idTarifa);
            return result.ToMinimalResult();
        }).WithTags("09 - Orden").RequireAuthorization("Cliente");
    }
}