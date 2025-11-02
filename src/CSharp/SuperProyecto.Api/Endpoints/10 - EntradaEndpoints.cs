namespace SuperProyecto.Api.Endpoints;

public static class EntradaEndpoints
{
    public static void MapEntradaEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/Entrada", (IEntradaService service) =>
        {
            var result = service.GetEntradas();
            return result.ToMinimalResult();
        }).WithTags("10 - Entrada").RequireAuthorization("Cliente");

        app.MapGet("/api/Entrada/{id}", (int id, IEntradaService service) =>
        {
            var result = service.DetalleEntrada(id);
            return result.ToMinimalResult();
        }).WithTags("10 - Entrada").RequireAuthorization("Cliente");

        app.MapGet("/api/Entrada/{id}/Qr", (int id, IEntradaService service) =>
        {
            var result = service.GetQr(id);
            return result.ToMinimalResult();
        }).WithTags("10 - Entrada").RequireAuthorization("Cliente");

        app.MapPut("/api/Entrada/qr/validar", (int id, IEntradaService service) =>
        {
            var result = service.ValidarQr(id);
            return result.ToMinimalResult();
        }).WithTags("10 - Entrada").RequireAuthorization("Organizador");
    }
}