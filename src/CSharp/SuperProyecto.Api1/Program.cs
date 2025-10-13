var builder = WebApplication.CreateBuilder(args);

#region DataBaseConnection
// 1) Leer appsettings.json
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
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
#endregion

conectionString += "Database=bd_boleteria;"; 

// Add services to the container.
#region Scoped
//Services
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<QrService>();

//Repos
builder.Services.AddScoped<IAdo>(sp => new Ado(conectionString));
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


#region TokenValidator
var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);

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
});
#endregion


#region CreateDataBase

string schemaDDL = Path.Combine(AppContext.BaseDirectory, "../../../../../../scripts/bd/MySQL/00 DDL.sql");
string schemaINSERTS = Path.Combine(AppContext.BaseDirectory, "../../../../../../scripts/bd/MySQL/01 INSERTS.sql");

string schemaSql = File.ReadAllText(schemaDDL) + File.ReadAllText(schemaINSERTS);

/* using (IDbConnection db = new MySqlConnection(conectionString))
{
    db.Open();

    db.Execute("DROP DATABASE IF EXISTS bd_boleteria; CREATE DATABASE bd_boleteria CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;");

    db.Execute("USE bd_boleteria; " + schemaSql);
} */
#endregion

builder.Services.AddAuthorization();

builder.Services.AddControllers(); // Añade controladores

#region Validators
builder.Services.AddFluentValidationAutoValidation(); //Habilita la validacion automatica
builder.Services.AddFluentValidationClientsideAdapters();

//Registramos los validadores para que puedan utilizar inyeccion de dependencias
builder.Services.AddValidatorsFromAssemblyContaining<ClienteValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<EntradaValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<EventoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<FuncionValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<LocalValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<OrdenValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<SectorValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<TarifaValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UsuarioValidator>();
#endregion

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

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

//app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

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