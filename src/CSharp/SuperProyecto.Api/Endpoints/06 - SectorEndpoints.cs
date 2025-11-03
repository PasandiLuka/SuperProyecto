namespace SuperProyecto.Api.Endpoints;

public static class SectorEndpoints
{
    public static void MapSectorEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/sectores/{idLocal}", (ISectorService service, int idLocal) =>
        {
            var result = service.GetSectores(idLocal);
            return result.ToMinimalResult();
        }).WithTags("06 - Sector").RequireAuthorization("Cliente");

        app.MapPut("/api/sectores/{id}", (int id, SectorDto sectorDto, ISectorService service) =>
        {
            var result = service.UpdateSector(sectorDto, id);
            return result.ToMinimalResult();
        }).WithTags("06 - Sector").RequireAuthorization("Organizador");
            
        app.MapPost("/api/locales({idLocal}/sectores)", (int idLocal, SectorDto sectorDto, ISectorService service) =>
        {
            var result = service.AltaSector(sectorDto, idLocal);
            return result.ToMinimalResult();
        }).WithTags("06 - Sector").RequireAuthorization("Organizador");
        
        app.MapDelete("/api/sectores/{id}", (int id, ISectorService service) =>
        {
            var result = service.DeleteSector(id);
            return result.ToMinimalResult();
        }).WithTags("06 - Sector").RequireAuthorization("Organizador");
    }
}