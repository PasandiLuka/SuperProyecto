using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using SuperProyecto.Core.IServices;

namespace SuperProyecto.Services.Service;

public class GetRolActualService : IGetRolActualService
{
    readonly IHttpContextAccessor? _httpContextAccessor;
    public GetRolActualService(IHttpContextAccessor httpContextAccessor) => _httpContextAccessor = httpContextAccessor;
    public string GetRolActual()
    {
        var User = _httpContextAccessor.HttpContext?.User;
        var rol = User.FindFirst(ClaimTypes.Role)?.Value;
        if (rol is null) return "Default";
        return rol;
    }
}