using SuperProyecto.Core.IServices;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
namespace SuperProyecto.Services.Service;

public class UrlConstructorService : IUrlConstructorService
{
    readonly LinkGenerator _linkGenerator;
    readonly IHttpContextAccessor _httpContextAccessor;

    public UrlConstructorService(LinkGenerator linkGenerator, IHttpContextAccessor httpContextAccessor)
    {
        _linkGenerator = linkGenerator;
        _httpContextAccessor = httpContextAccessor;
    }

    public string GenerarQrUrl(int id)
    {
        var httpContext = _httpContextAccessor.HttpContext!;
        var routeValues = new RouteValueDictionary { ["id"] = id };
        var qrUrl = _linkGenerator.GetUriByAddress(httpContext, "ValidarQr", routeValues);
        return qrUrl!;
    }
}