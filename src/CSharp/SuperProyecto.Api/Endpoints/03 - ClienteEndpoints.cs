namespace SuperProyecto.Api.Endpoints;

public static class ClienteEndpoints
{
    public static void MapClienteEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/ClienteTest", (IClienteService service) =>
        {
            var result = service.GetClientes();
            return result.ToMinimalResult();
        }).WithTags("03 - Cliente");
        
        app.MapGet("/api/Cliente", (IClienteService service) =>
        {
            var result = service.GetClientes();
            return result.ToMinimalResult();
        }).WithTags("03 - Cliente").RequireAuthorization("Cliente");

        app.MapGet("/api/Cliente/{id}", (int documento, IClienteService service) =>
        {
            var result = service.DetalleCliente(documento);
            return result.ToMinimalResult();
        }).WithTags("03 - Cliente").RequireAuthorization("Cliente");

        app.MapPut("/api/Cliente/{id}", (int id, ClienteDtoUpdate clienteDto, IClienteService service) =>
        {
            var result = service.UpdateCliente(clienteDto, id);
            return result.ToMinimalResult();
        }).WithTags("03 - Cliente").RequireAuthorization("Cliente");

        app.MapPost("/api/Cliente", (ClienteDtoAlta cliente, IClienteService service) =>
        {
            var result = service.AltaCliente(cliente);
            return result.ToMinimalResult();
        }).WithTags("03 - Cliente").RequireAuthorization("Cliente");
    }
}