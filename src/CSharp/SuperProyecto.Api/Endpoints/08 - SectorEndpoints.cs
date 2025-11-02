namespace SuperProyecto.Api.Endpoints;

public static class SectorEndpoints
{
    public static void MapSectorEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/Sector", (ISectorService service) =>
        {
            var result = service.GetSectores();
            return result.ToMinimalResult();
        }).WithTags("08 - Sector").RequireAuthorization("Cliente");
            
        app.MapGet("/api/Sector/{id}", (int id, ISectorService service) =>
        {
            var result = service.DetalleSector(id);
            return result.ToMinimalResult();
        }).WithTags("08 - Sector").RequireAuthorization("Cliente");

        app.MapPut("/api/Sector/{id}", (int id, SectorDto sectorDto, ISectorService service) =>
        {
            var result = service.UpdateSector(sectorDto, id);
            return result.ToMinimalResult();
        }).WithTags("08 - Sector").RequireAuthorization("Organizador");
            
        app.MapPost("/api/Sector", (SectorDto sectorDto, ISectorService service) =>
        {
            var result = service.AltaSector(sectorDto);
            return result.ToMinimalResult();
        }).WithTags("08 - Sector").RequireAuthorization("Organizador");
        
        app.MapDelete("/api/Sector/{id}", (int id, ISectorService service) =>
        {
            var result = service.DeleteSector(id);
            return result.ToMinimalResult();
        }).WithTags("08 - Sector").RequireAuthorization("Organizador");
    }
}