//Referencias a los proyectos
using SuperProyecto.Services.Service;
using SuperProyecto.Services.Validators;
using SuperProyecto.Dapper;
using SuperProyecto.Core.IServices;
using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.DTO;
using SuperProyecto.Core.Enums;
using SuperProyecto.Api.Enpoints;

//Paquetes api
using Microsoft.OpenApi.Models;
using SuperProyecto.Web.Helpers;

//Paquetes para la autenticacion por token
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SuperProyecto.Api.Helper;
using SuperProyecto.Core.Entidades;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();

//Servicios para implementar la autenticacion y autorizacion
#region Auth
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true, // que valide la caducidad del token
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
        ),
        ClockSkew = TimeSpan.Zero //tiempo de tolerancia por defecto de 5mins antes de inhabilitar el token
    };
}); // Ejemplo con JWT
builder.Services.AddAuthorization();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<AuthService>();
#endregion


//Servicio y repositorio que me permite entablar la conexion con la base de datos
#region Configuracion Bd
//Los creo como singleton ya que no requiero que a cada request se cree una nueva instancia
builder.Services.AddSingleton<IDataBaseConnectionService, DataBaseConnectionService>();
builder.Services.AddSingleton<IAdo, Ado>();
#endregion


//Instanciamos todos los repositorios
#region Repositorios

builder.Services.AddScoped<IRepoQr, RepoQr>();
builder.Services.AddScoped<IRepoToken, RepoToken>();
builder.Services.AddScoped<IRepoUsuario, RepoUsuario>();
builder.Services.AddScoped<IRepoCliente, RepoCliente>();
builder.Services.AddScoped<IRepoEntrada, RepoEntrada>();
builder.Services.AddScoped<IRepoEvento, RepoEvento>();
builder.Services.AddScoped<IRepoFuncion, RepoFuncion>();
builder.Services.AddScoped<IRepoLocal, RepoLocal>();
builder.Services.AddScoped<IRepoOrden, RepoOrden>();
builder.Services.AddScoped<IRepoSector, RepoSector>();
builder.Services.AddScoped<IRepoTarifa, RepoTarifa>();
#endregion


//Instancio los validadores
#region Validadores
builder.Services.AddScoped<ClienteValidator>();
builder.Services.AddScoped<EntradaValidator>();
builder.Services.AddScoped<EventoValidator>();
builder.Services.AddScoped<FuncionValidator>();
builder.Services.AddScoped<LocalValidator>();
builder.Services.AddScoped<OrdenValidator>();
builder.Services.AddScoped<SectorValidator>();
builder.Services.AddScoped<TarifaValidator>();
builder.Services.AddScoped<UsuarioValidator>();
#endregion


//Instaciamos los servicios
#region Servicios
builder.Services.AddScoped<IQrService, QrService>();
builder.Services.AddScoped<IUrlConstructor, UrlConstructor>();
//Necesario para poder inyectar el HttpContext y obtener el usuario logueado
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IEntradaService, EntradaService>();
builder.Services.AddScoped<IEventoService, EventoService>();
builder.Services.AddScoped<IFuncionService, FuncionService>();
builder.Services.AddScoped<ILocalService, LocalService>();
builder.Services.AddScoped<IOrdenService, OrdenService>();
builder.Services.AddScoped<ISectorService, SectorService>();
builder.Services.AddScoped<ITarifaService, TarifaService>();
#endregion


// Configuracion swagger para implementar la autenticacion por token
builder.Services.AddSwaggerGen(c =>
{
    //Referenciamos el token

    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SuperProyecto API", Version = "v1" });

    // Configuración JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingrese 'Bearer' seguido del token JWT"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();



if (app.Environment.IsDevelopment())
{
    /* app.MapOpenApi(); */
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () => "Hello World!").WithTags("HelloWorld");

#region EndPoints
app.MapGet("/api/Usuario/roles", (IUsuarioService service) =>
{
    var result = service.ObtenerRoles();
    return result.ToMinimalResult();
}).WithTags("Usuario/Auth");
app.MapPost("/api/auth/register", (UsuarioDto usuarioDto, IUsuarioService service) =>
{
    var result = service.AltaUsuario(usuarioDto);
    return result.ToMinimalResult();
}).WithTags("Usuario");
app.MapPut("/api/Usuario/{id}/rol", (int id, ERol nuevoRol, IUsuarioService service) =>
{
    var result = service.ActualizarRol(id, nuevoRol);
    return result.ToMinimalResult();
}).WithTags("Usuario");
#region Usuario

#endregion

#region Cliente
    app.MapGet("/api/Cliente", (IClienteService service) =>
    {
        var result = service.GetClientes();
        return result.ToMinimalResult();
    }).WithTags("Cliente");/* .RequireAuthorization("Cliente", "Administrador"); */

    app.MapGet("/api/Cliente/{id}", (int id, IClienteService service) =>
    {
        var result = service.DetalleCliente(id);
        return result.ToMinimalResult();
    }).WithTags("Cliente");/* .RequireAuthorization("Cliente", "Administrador"); */

    app.MapPut("/api/Cliente/{id}", (int id, ClienteDto clienteDto, IClienteService service) =>
    {
        var result = service.UpdateCliente(clienteDto, id);
        return result.ToMinimalResult();
    }).WithTags("Cliente");/* .RequireAuthorization("Cliente", "Administrador"); */

    app.MapPost("/api/Cliente", (ClienteDto clienteDto, IClienteService service) =>
    {
        var result = service.AltaCliente(clienteDto);
        return result.ToMinimalResult();
    }).WithTags("Cliente");/* .RequireAuthorization("Cliente", "Administrador"); */
#endregion

#region Entrada
    /* app.MapGet("/api/Entrada", (IEntradaService service) =>
    {
        var result = service.GetEntradas();
        return result.ToMinimalResult();
    }).WithTags("Entrada").RequireAuthorization("Cliente", "Administrador");

    app.MapGet("/api/Entrada/{id}", (int id, IEntradaService service) =>
    {
        var entrada = service.DetalleEntrada(id);
        return entrada is not null ? Results.Ok(entrada) : Results.NotFound();
    }).WithTags("Entrada").RequireAuthorization("Cliente", "Administrador");

    app.MapGet("/api/Entrada/{id}/Qr", (int id, IEntradaService service) =>
    {
        var qr = service.GetQr(id);
        return qr is not null ? Results.File(qr, "image/png") : Results.NotFound();
    }).WithTags("Entrada").RequireAuthorization("Cliente", "Administrador");

    app.MapPut("/api/Entrada/qr/validar", (int id, IEntradaService service) =>
    {
        var error = service.ValidarQr(id);
        return error is null ? Results.Ok() : Results.BadRequest(error);
    }).WithTags("Entrada").RequireAuthorization("Organizador", "Administrador"); */
#endregion

#region Evento
    app.MapGet("/api/Evento", (IEventoService service) =>
    {
        var result = service.GetEventos();
        return result.ToMinimalResult();
    }).WithTags("Evento").RequireAuthorization("Cliente", "Administrador");

    app.MapGet("/api/Evento/{id}", (int id, IEventoService service) =>
    {
        var result = service.DetalleEvento(id);
        return result.ToMinimalResult();
    }).WithTags("Evento").RequireAuthorization("Cliente", "Administrador");

    app.MapPut("/api/Evento/{id}", (int id, EventoDto eventoDto, IEventoService service) =>
    {
        var result = service.UpdateEvento(eventoDto, id);
        return result.ToMinimalResult();
    }).WithTags("Evento").RequireAuthorization("Organizador", "Administrador");

    app.MapPost("/api/Evento", (EventoDto eventoDto, IEventoService service) =>
    {
        var result = service.AltaEvento(eventoDto);
        return result.ToMinimalResult();
    }).WithTags("Evento").RequireAuthorization("Organizador", "Administrador");
#endregion

#region Funcion
    app.MapGet("/api/Funcion", (IFuncionService service) =>
    {
        var result = service.GetFunciones();
        return result.ToMinimalResult();
    }).WithTags("Funcion").RequireAuthorization("Cliente", "Administrador");
    
    app.MapGet("/api/Funcion/{id}", (int id, IFuncionService service) =>
    {
        var result = service.DetalleFuncion(id);
        return result.ToMinimalResult();
    }).WithTags("Funcion").RequireAuthorization("Cliente", "Administrador");

    app.MapPut("/api/Funcion/{id}", (int id, FuncionDto funcionDto, IFuncionService service) =>
    {
        var result = service.UpdateFuncion(funcionDto, id);
        return result.ToMinimalResult();
    }).WithTags("Funcion").RequireAuthorization("Organizador", "Administrador");
        
    app.MapPost("/api/Funcion", (FuncionDto funcionDto, IFuncionService service) =>
    {
        var result = service.AltaFuncion(funcionDto);
        return result.ToMinimalResult();
    }).WithTags("Funcion").RequireAuthorization("Organizador", "Administrador");
#endregion

#region Local
    app.MapGet("/api/Local", (ILocalService service) =>
    {
        var result = service.GetLocales();
        return result.ToMinimalResult();
    }).WithTags("Local").RequireAuthorization("Cliente", "Administrador");

    app.MapGet("/api/Local/{id}", (int id, ILocalService service) =>
    {
        var result = service.DetalleLocal(id);
        return result.ToMinimalResult();
    }).WithTags("Local").RequireAuthorization("Cliente", "Administrador");
        
        app.MapPut("/api/Local/{id}", (int id, LocalDto localDto, ILocalService service) =>
        {
            var result = service.UpdateLocal(localDto, id);
            return result.ToMinimalResult();
        }).WithTags("Local").RequireAuthorization("Organizador", "Administrador");

    app.MapPost("/api/Local", (LocalDto localDto, ILocalService service) =>
    {
        var result = service.AltaLocal(localDto);
        return result.ToMinimalResult();
    }).WithTags("Local").RequireAuthorization("Organizador", "Administrador");
        
        app.MapDelete("/api/Local/{id}", (int id, ILocalService service) =>
        {
            var result = service.DeleteLocal(id);
            return result.ToMinimalResult();
        }).WithTags("Local").RequireAuthorization("Administrador");
#endregion

#region Orden
    app.MapGet("/api/Orden", (IOrdenService service) =>
    {
        var result = service.GetOrdenes();
        return result.ToMinimalResult();
    }).WithTags("Orden").RequireAuthorization("Cliente", "Administrador");
        
        app.MapGet("/api/Orden/{id}", (int id, IOrdenService service) =>
        {
            var result = service.DetalleOrden(id);
            return result.ToMinimalResult();
        }).WithTags("Orden").RequireAuthorization("Cliente", "Administrador");

    app.MapPost("/api/Orden", (OrdenDto ordenDto, IOrdenService service) =>
    {
        var result = service.AltaOrden(ordenDto);
        return result.ToMinimalResult();
    }).WithTags("Orden").RequireAuthorization("Organizador", "Administrador");
        
        app.MapPut("/api/Orden/{id}/pagar", (int id, IOrdenService service) =>
        {
            var result = service.PagarOrden(id);
            return result.ToMinimalResult();
    }).WithTags("Orden").RequireAuthorization("Cliente", "Administrador");
#endregion

#region Sector
app.MapGet("/api/Sector", (ISectorService service) =>
    {
        var result = service.GetSectores();
        return result.ToMinimalResult();
    }).WithTags("Sector").RequireAuthorization("Cliente", "Administrador");
        
    app.MapGet("/api/Sector/{id}", (int id, ISectorService service) =>
    {
        var result = service.DetalleSector(id);
        return result.ToMinimalResult();
    }).WithTags("Sector").RequireAuthorization("Cliente", "Administrador");

    app.MapPut("/api/Sector/{id}", (int id, SectorDto sectorDto, ISectorService service) =>
    {
        var result = service.UpdateSector(sectorDto, id);
        return result.ToMinimalResult();
    }).WithTags("Sector").RequireAuthorization("Organizador", "Administrador");
        
    app.MapPost("/api/Sector", (SectorDto sectorDto, ISectorService service) =>
    {
        var result = service.AltaSector(sectorDto);
        return result.ToMinimalResult();
    }).WithTags("Sector").RequireAuthorization("Organizador", "Administrador");
    
    app.MapDelete("/api/Sector/{id}", (int id, ISectorService service) =>
    {
        var result = service.DeleteSector(id);
        return result.ToMinimalResult();
    }).WithTags("Sector").RequireAuthorization("Administrador");
#endregion

#region Tarifa
    app.MapGet("/api/Tarifa", (ITarifaService service) =>
    {
        var result = service.GetTarifas();
        return result.ToMinimalResult();
    }).WithTags("Tarifa").RequireAuthorization("Cliente", "Administrador");

    app.MapGet("/api/Tarifa/{id}", (int id, ITarifaService service) =>
    {
        var result = service.DetalleTarifa(id);
        return result.ToMinimalResult();
    }).WithTags("Tarifa").RequireAuthorization("Cliente", "Administrador");

    app.MapPut("/api/Tarifa/{id}", (int id, TarifaDto tarifaDto, ITarifaService service) =>
    {
        var result = service.UpdateTarifa(tarifaDto, id);
        return result.ToMinimalResult();
    }).WithTags("Tarifa").RequireAuthorization("Organizador", "Administrador");
            
    app.MapPost("/api/Tarifa", (TarifaDto tarifaDto, ITarifaService service) =>
    {
        var result = service.AltaTarifa(tarifaDto);
        return result.ToMinimalResult();
    }).WithTags("Tarifa").RequireAuthorization("Organizador", "Administrador");
#endregion

#endregion

app.Run();

//ClienteRequestDto
//ClienteResponseDto

//Patron CQRS (Command Query Responsibility Segregation)