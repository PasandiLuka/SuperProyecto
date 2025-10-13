using SuperProyecto.Services.Service;

using SuperProyecto.Core.Persistencia;
using SuperProyecto.Dapper;
using SuperProyecto.Core.Entidades;

using MySqlConnector;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;


#region DataBaseConnection
// 1) Leer appsettings.json
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile(
        "appsettings.json",
        optional: false
        )
    .Build();

// 2) Obtener todas las cadenas de conexión
var connectionStrings = configuration.GetSection("ConnectionStrings").GetChildren();

string conectionString = null;

// 3) Probar cada conexión
foreach (var cs in connectionStrings)   
{
    Console.WriteLine($"🔄 Probando: {cs.Key}");
    if (ProbarConexion(cs.Value))
    {
        conectionString = cs.Value;
        Console.WriteLine($"✅ Conexión válida: {cs.Key}");
        break;
    }
    else
    {
        Console.WriteLine($"❌ Falló: {cs.Key}");
    }
}

// 4) Resultado final
if (conectionString == null)
{
    throw new ArgumentException("⚠ Ninguna cadena de conexión funcionó.");
}
else
{
    Console.WriteLine($"✔ Usando conexión: {conectionString}");
    // acá podrías guardarla en una variable global o inyectarla en servicios
}

conectionString += "Database=bd_boleteria;";

#endregion


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();

builder.Services.AddScoped<IAdo>(sp => new Ado(conectionString));

builder.Services.AddScoped<IRepoCliente, RepoCliente>();

builder.Services.AddScoped<ClienteService>();

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


app.MapGet("/", () => "Hello World!").WithName("HelloWorld");

#region EndPoints

#region Cliente
    app.MapGet("/api/Cliente", (ClienteService service) =>
    {
        var clientes = service.GetClientes();
        return Results.Ok(clientes);
    }).WithTags("Cliente");

    app.MapGet("/api/Cliente/{id}", (int id, ClienteService service) =>
    {
        var clientes = service.DetalleCliente(id);
        return clientes is not null ?  Results.Ok(clientes) : Results.NotFound();
    }).WithTags("Cliente");

    app.MapPut("/api/Cliente/{id}", (int id, Cliente cliente, ClienteService service) =>
    {
        service.UpdateCliente(cliente, id);
        return Results.Ok();
    }).WithTags("Cliente");

    app.MapPost("/api/Cliente", (Cliente cliente, ClienteService service) =>
    {
        service.AltaCliente(cliente);
        return Results.Created();
    }).WithTags("Cliente");
#endregion
    
#endregion





app.Run();


static bool ProbarConexion(string cs)
{
    try
    {
        using var conn = new MySqlConnection(cs);
        conn.Open();
        return true;
    }
    catch
    {
        return false;
    }
}