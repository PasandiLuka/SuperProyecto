namespace SuperProyecto.Api.Endpoints;
public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/auth/login", (Core.DTO.LoginRequest loginRequest, AuthService authService) =>
        {
            var result = authService.Login(loginRequest);
            return result.ToMinimalResult();
        }).WithTags("02 - Auth");

        app.MapGet("/api/auth/me", (AuthService authService) =>
        {
            var result = authService.Me();
            return result.ToMinimalResult();
        }).WithTags("02 - Auth").RequireAuthorization();

        app.MapPost("/api/auth/refresh", (RefreshTokenRequest refreshTokenRequest, AuthService authService) =>
        {
            var result = authService.RefreshToken(refreshTokenRequest);
            return result.ToMinimalResult();
        }).WithTags("02 - Auth");

        app.MapPut("/api/auth/logout", (AuthService authService) =>
        {
            var result = authService.Logout();
            return result.ToMinimalResult();
        }).WithTags("02 - Auth").RequireAuthorization();
    }
}