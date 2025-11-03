namespace SuperProyecto.Api.Endpoints;

public static class LocalEndpoints
{
    public static void MapLocalEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/locales", (ILocalService service) =>
        {
            var result = service.GetLocales();
            return result.ToMinimalResult();
        }).WithTags("05 - Local").RequireAuthorization("Cliente");

        app.MapGet("/api/locales/{id}", (int id, ILocalService service) =>
        {
            var result = service.DetalleLocal(id);
            return result.ToMinimalResult();
        }).WithTags("05 - Local").RequireAuthorization("Cliente");

        app.MapPut("/api/locales/{id}", (int id, LocalDto localDto, ILocalService service) =>
        {
            var result = service.UpdateLocal(localDto, id);
            return result.ToMinimalResult();
        }).WithTags("05 - Local").RequireAuthorization("Organizador");

        app.MapPost("/api/locales", (LocalDto localDto, ILocalService service) =>
        {
            var result = service.AltaLocal(localDto);
            return result.ToMinimalResult();
        }).WithTags("05 - Local").RequireAuthorization("Organizador");
            
        app.MapDelete("/api/locales/{id}", (int id, ILocalService service) =>
        {
            var result = service.DeleteLocal(id);
            return result.ToMinimalResult();
        }).WithTags("05 - Local").RequireAuthorization("Organizador");
    }
}