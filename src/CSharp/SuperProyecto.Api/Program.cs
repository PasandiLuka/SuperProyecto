using SuperProyecto.Services.Service;

using SuperProyecto.Core.Persistencia;
using SuperProyecto.Dapper;
using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.IServices;
using Microsoft.OpenApi.Models;
using SuperProyecto.Web.Helpers;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();


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


//Intaciamos los servicios
#region Servicios
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<IQrService, QrService>();
builder.Services.AddScoped<IUrlConstructor, UrlConstructor>();
//Necesario para poder inyectar el HttpContext y obtener el usuario logueado
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IEntradaService, EntradaService>();
builder.Services.AddScoped<IEventoService, EventoService>();
builder.Services.AddScoped<IFuncionService, FuncionService>();
builder.Services.AddScoped<ILocalService, LocalService>();
builder.Services.AddScoped<IOrdenService, OrdenService>();
builder.Services.AddScoped<ISectorService, SectorService>();
builder.Services.AddScoped<ITarifaService, TarifaService>();
#endregion


// Configuracio swagger para implementar la autenticacion por token
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

app.UseSwagger();
app.UseSwaggerUI();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.MapGet("/", () => "Hello World!").WithTags("HelloWorld");

#region EndPoints

#region Cliente
    app.MapGet("/api/Cliente", (IClienteService service) =>
    {
        var clientes = service.GetClientes();
        return Results.Ok(clientes);
    }).WithTags("Cliente");

    app.MapGet("/api/Cliente/{id}", (int id, IClienteService service) =>
    {
        var clientes = service.DetalleCliente(id);
        return clientes is not null ?  Results.Ok(clientes) : Results.NotFound();
    }).WithTags("Cliente");

    app.MapPut("/api/Cliente/{id}", (int id, Cliente cliente, IClienteService service) =>
    {
        service.UpdateCliente(cliente, id);
        return Results.Ok();
    }).WithTags("Cliente");

    app.MapPost("/api/Cliente", (Cliente cliente, IClienteService service) =>
    {
        service.AltaCliente(cliente);
        return Results.Created();
    }).WithTags("Cliente");
#endregion

#region Entrada
    app.MapGet("/api/Entrada", (IEntradaService service) =>
    {
        var entradas = service.GetEntradas();
        return Results.Ok(entradas);
    }).WithTags("Entrada");

    app.MapGet("/api/Entrada/{id}", (int id, IEntradaService service) =>
    {
        var entrada = service.DetalleEntrada(id);
        return entrada is not null ? Results.Ok(entrada) : Results.NotFound();
    }).WithTags("Entrada");

    app.MapGet("/api/Entrada/{id}/Qr", (int id, IEntradaService service) =>
    {
        var qr = service.GetQr(id);
        return qr is not null ? Results.File(qr, "image/png") : Results.NotFound();
    }).WithTags("Entrada");

    app.MapPut("/api/Entrada/qr/validar", (int id, IEntradaService service) =>
    {
        var error = service.ValidarQr(id);
        return error is null ? Results.Ok() : Results.BadRequest(error);
    }).WithTags("Entrada");
#endregion

#region Evento
    app.MapGet("/api/Evento", (IEventoService service) =>
    {
        var eventos = service.GetEventos();
        return Results.Ok(eventos);
    }).WithTags("Evento");

    app.MapGet("/api/Evento/{id}", (int id, IEventoService service) =>
    {
        var evento = service.DetalleEvento(id);
        return evento is not null ? Results.Ok(evento) : Results.NotFound();
    }).WithTags("Evento");

    app.MapPut("/api/Evento/{id}", (int id, Evento evento, IEventoService service) =>
    {
        service.UpdateEvento(evento, id);
        return Results.Ok();
    }).WithTags("Evento");

    app.MapPost("/api/Evento", (Evento evento, IEventoService service) =>
    {
        service.AltaEvento(evento);
        return Results.Created();
    }).WithTags("Evento");
#endregion

#region Funcion
    app.MapGet("/api/Funcion", (IFuncionService service) =>
    {
        var funciones = service.GetFunciones();
        return Results.Ok(funciones);
    }).WithTags("Funcion");

    app.MapGet("/api/Funcion/{id}", (int id, IFuncionService service) =>
    {
        var funcion = service.DetalleFuncion(id);
        return funcion is not null ? Results.Ok(funcion) : Results.NotFound();
    }).WithTags("Funcion");

    app.MapPut("/api/Funcion/{id}", (int id, Funcion funcion, IFuncionService service) =>
    {
        service.UpdateFuncion(funcion, id);
        return Results.Ok();
    }).WithTags("Funcion");

    app.MapPost("/api/Funcion", (Funcion funcion, IFuncionService service) =>
    {
        service.AltaFuncion(funcion);
        return Results.Created();
    }).WithTags("Funcion");
#endregion

#region Local
    app.MapGet("/api/Local", (ILocalService service) =>
    {
        var locales = service.GetLocales();
        return Results.Ok(locales);
    }).WithTags("Local");
    app.MapGet("/api/Local/{id}", (int id, ILocalService service) =>
    {
        var local = service.DetalleLocal(id);
        return local is not null ? Results.Ok(local) : Results.NotFound();
    }).WithTags("Local");
    app.MapPut("/api/Local/{id}", (int id, Local local, ILocalService service) =>
    {
        service.UpdateLocal(local, id);
        return Results.Ok();
    }).WithTags("Local");
    app.MapPost("/api/Local", (Local local, ILocalService service) =>
    {
        service.AltaLocal(local);
        return Results.Created();
    }).WithTags("Local");
    app.MapDelete("/api/Local/{id}", (int id, ILocalService service) =>
    {
        service.DeleteLocal(id);
        return Results.Ok();
    }).WithTags("Local");
#endregion

#region Orden
    app.MapGet("/api/Orden", (IOrdenService service) =>
    {
        var ordenes = service.GetOrdenes();
        return Results.Ok(ordenes);
    }).WithTags("Orden");
    app.MapGet("/api/Orden/{id}", (int id, IOrdenService service) =>
    {
        var orden = service.DetalleOrden(id);
        return orden is not null ? Results.Ok(orden) : Results.NotFound();
    }).WithTags("Orden");
    app.MapPost("/api/Orden", (Orden orden, IOrdenService service) =>
    {
        service.AltaOrden(orden);
        return Results.Created();
    }).WithTags("Orden");
    app.MapPut("/api/Orden/{id}/pagar", (int id, IOrdenService service) =>
    {
        service.PagarOrden(id);
        return Results.Ok();
    }).WithTags("Orden");
#endregion

#region Sector
    app.MapGet("/api/Sector", (ISectorService service) =>
    {
        var sectores = service.GetSectores();
        return Results.Ok(sectores);
    }).WithTags("Sector");
    app.MapGet("/api/Sector/{id}", (int id, ISectorService service) =>
    {
        var sector = service.DetalleSector(id);
        return sector is not null ? Results.Ok(sector) : Results.NotFound();
    }).WithTags("Sector");
    app.MapPut("/api/Sector/{id}", (int id, Sector sector, ISectorService service) =>
    {
        service.UpdateSector(sector, id);
        return Results.Ok();
    }).WithTags("Sector");
    app.MapPost("/api/Sector", (Sector sector, ISectorService service) =>
    {
        service.AltaSector(sector);
        return Results.Created();
    }).WithTags("Sector");
    app.MapDelete("/api/Sector/{id}", (int id, ISectorService service) =>
    {
        service.DeleteSector(id);
        return Results.Ok();
    }).WithTags("Sector");
#endregion

#region Tarifa
        app.MapGet("/api/Tarifa", (ITarifaService service) =>
        {
            var tarifas = service.GetTarifas();
            return Results.Ok(tarifas);
        }).WithTags("Tarifa");
        app.MapGet("/api/Tarifa/{id}", (int id, ITarifaService service) =>
        {
            var tarifa = service.DetalleTarifa(id);
            return tarifa is not null ? Results.Ok(tarifa) : Results.NotFound();
        }).WithTags("Tarifa");
        app.MapPut("/api/Tarifa/{id}", (int id, Tarifa tarifa, ITarifaService service) =>
        {
            service.UpdateTarifa(tarifa, id);
            return Results.Ok();
        }).WithTags("Tarifa");
        app.MapPost("/api/Tarifa", (Tarifa tarifa, ITarifaService service) =>
        {
            service.AltaTarifa(tarifa);
            return Results.Created();
        }).WithTags("Tarifa");
#endregion

#endregion

app.Run();