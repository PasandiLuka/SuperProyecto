namespace SuperProyecto.Api.Endpoints;

public static class ClienteEndpoints
{
    public static void MapClienteEndpoints(this IEndpointRouteBuilder app)
    {        
        app.MapGet("/api/clientes", (IClienteService service) =>
        {
            var result = service.GetClientes();
            return result.ToMinimalResult();
        }).WithTags("03 - Cliente").RequireAuthorization("Cliente");

        app.MapGet("/api/clientes/{id}", (int id, IClienteService service) =>
        {
            var result = service.DetalleCliente(id);
            return result.ToMinimalResult();
        }).WithTags("03 - Cliente").RequireAuthorization("Cliente");

        app.MapPut("/api/clientes/{id}", (int id, ClienteDtoUpdate clienteDto, IClienteService service) =>
        {
            var result = service.UpdateCliente(clienteDto, id);
            return result.ToMinimalResult();
        }).WithTags("03 - Cliente").RequireAuthorization("Cliente");

        app.MapPost("/api/clientes", (ClienteDtoAlta cliente, IClienteService service) =>
        {
            var result = service.AltaCliente(cliente);
            return result.ToMinimalResult();
        }).WithTags("03 - Cliente").RequireAuthorization("Cliente");
    }
}