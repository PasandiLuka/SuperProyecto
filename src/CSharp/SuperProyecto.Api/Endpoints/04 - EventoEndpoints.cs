namespace SuperProyecto.Api.Endpoints;

public static class EventoEndpoints
{
    public static void MapEventoEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/Evento", (IEventoService service) =>
        {
            var result = service.GetEventos();
            return result.ToMinimalResult();
        }).WithTags("04 - Evento").RequireAuthorization("Cliente");

        app.MapGet("/api/Evento/{id}", (int id, IEventoService service) =>
        {
            var result = service.DetalleEvento(id);
            return result.ToMinimalResult();
        }).WithTags("04 - Evento").RequireAuthorization("Cliente");
        
        app.MapPut("/api/EventoTest/{id}", (int id, EventoDto eventoDto, IEventoService service) =>
        {
            var result = service.UpdateEvento(eventoDto, id);
            return result.ToMinimalResult();
        }).WithTags("04 - Evento");

        app.MapPut("/api/Evento/{id}", (int id, EventoDto eventoDto, IEventoService service) =>
        {
            var result = service.UpdateEvento(eventoDto, id);
            return result.ToMinimalResult();
        }).WithTags("04 - Evento").RequireAuthorization("Organizador");

        app.MapPut("/api/Evento/{id}/cancelar", (int id, IEventoService service) =>
        {
            var result = service.CancelarEvento(id);
            return result.ToMinimalResult();
        }).WithTags("04 - Evento").RequireAuthorization("Organizador");
        
        app.MapPut("/api/Evento/{id}/publicar", (int id, IEventoService service) =>
        {
            var result = service.PublicarEvento(id);
            return result.ToMinimalResult();
        }).WithTags("04 - Evento").RequireAuthorization("Organizador");

        app.MapPost("/api/Evento", (EventoDto eventoDto, IEventoService service) =>
        {
            var result = service.AltaEvento(eventoDto);
            return result.ToMinimalResult();
        }).WithTags("04 - Evento").RequireAuthorization("Organizador");
    }
}