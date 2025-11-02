#region Usings
//Carpeta donde se encuentran los endpoints
using SuperProyecto.Api.Endpoints;

//Referencias a los proyectos
using SuperProyecto.Dapper;
using SuperProyecto.Services.Validators;

//Paquetes api
using Microsoft.OpenApi.Models;

//Paquetes para la autenticacion por token
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
#endregion


var builder = WebApplication.CreateBuilder(args);


#region Auth
//Servicios para implementar la autenticacion y autorizacion por tokens JWT
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
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Administrador", policy =>
        policy.RequireRole("Administrador"));

    options.AddPolicy("Cliente", policy =>
        policy.RequireRole("Cliente", "Administrador"));

    options.AddPolicy("Organizador", policy =>
        policy.RequireRole("Organizador", "Administrador"));
});
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<AuthService>();
#endregion


#region Configuracion Bd
//Servicio y repositorio que me permite entablar la conexion con la base de datos y crear la misma.
builder.Services.AddSingleton<DataBaseCreationService>();
builder.Services.AddScoped<IGetRolActualService, GetRolActualService>();
builder.Services.AddSingleton<IDataBaseConnectionService, DataBaseConnectionService>();

//Este hostedService lo que hace es ejecutarse una unica vez cuando se inicia la aplicacion,
//esto me permite crear la base de datos de manera automatica.
builder.Services.AddHostedService<DatabaseInitHostedService>();
builder.Services.AddScoped<IAdo, Ado>();
#endregion


#region Repositorios
//Instanciamos todos los repositorios
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


#region Validadores
//Instancio los validadores
builder.Services.AddScoped<ClienteDtoAltaValidator>();
builder.Services.AddScoped<ClienteDtoUpdateValidator>();
builder.Services.AddScoped<EntradaValidator>();
builder.Services.AddScoped<EventoValidator>();
builder.Services.AddScoped<FuncionValidator>();
builder.Services.AddScoped<LocalValidator>();
builder.Services.AddScoped<OrdenValidator>();
builder.Services.AddScoped<SectorValidator>();
builder.Services.AddScoped<TarifaValidator>();
builder.Services.AddScoped<UsuarioValidator>();
#endregion


#region Servicios
//Instaciamos los servicios
builder.Services.AddScoped<IQrService, QrService>();
builder.Services.AddScoped<IUrlConstructorService, UrlConstructorService>();
//Necesario para poder inyectar el HttpContext en el AuthService y obtener el usuario logueado
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

//Esto permite al swagger mapear todos los endpoints
builder.Services.AddEndpointsApiExplorer();

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

//Habilita nuestra autentificacion
app.UseAuthentication();
app.UseAuthorization();

//algo hace...
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

#region Endpoints
app.MapGet("/", () => "Hello World!").WithTags("00 - HelloWorld");
app.MapUsuarioEndpoints();
app.MapAuthEndpoints();
app.MapClienteEndpoints();
app.MapEventoEndpoints();
app.MapLocalEndpoints();
app.MapTarifaEndpoints();
app.MapFuncionEndpoints();
app.MapSectorEndpoints();
app.MapOrdenEndpoints();
app.MapEntradaEndpoints();
#endregion


app.Run();

//ClienteRequestDto
//ClienteResponseDto

//Patron CQRS (Command Query Responsibility Segregation)