using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SuperProyecto.Core.IServices;

namespace SuperProyecto.Web.Helpers
{
    public class UrlConstructor : IUrlConstructor
    {
        readonly LinkGenerator _linkGenerator;
        readonly IHttpContextAccessor _httpContextAccessor;

        public UrlConstructor(LinkGenerator linkGenerator, IHttpContextAccessor httpContextAccessor)
        {
            _linkGenerator = linkGenerator;
            _httpContextAccessor = httpContextAccessor;
        }

        public string GenerarQrUrl(int id)
        {
            var httpContext = _httpContextAccessor.HttpContext!;
            var qrUrl = _linkGenerator.GetUriByName(httpContext, "ValidarQr", new { id });
            return qrUrl!;
        }
    }
}