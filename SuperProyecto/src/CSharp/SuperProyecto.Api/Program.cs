using System.Data;
using Swashbuckle.SwaggerUi;
var builder = WebApplication.CreateBuilder(args);

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


/* string conection = @"Server=localhost;Uid=root;Pwd=48460731"; */

string schemaDDL = Path.Combine(AppContext.BaseDirectory, "../../../../../../scripts/bd/MySQL/00 DDL.sql");
string schemaINSERTS = Path.Combine(AppContext.BaseDirectory, "../../../../../../scripts/bd/MySQL/01 INSERTS.sql");

string schemaSql = File.ReadAllText(schemaDDL) + File.ReadAllText(schemaINSERTS);

using (IDbConnection db = new MySqlConnection(conectionString))
{
    db.Open();

    db.Execute("DROP DATABASE IF EXISTS bd_boleteria; CREATE DATABASE bd_boleteria CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;");

    db.Execute("USE bd_boleteria; " + schemaSql);
}

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

#region ReposScoped
    builder.Services.AddScoped<IAdo>(sp => new Ado(conectionString));
    builder.Services.AddScoped<IRepoCliente, RepoCliente>();
    builder.Services.AddScoped<IRepoEntrada, RepoEntrada>();
    builder.Services.AddScoped<IRepoEvento, RepoEvento>();
    builder.Services.AddScoped<IRepoFuncion, RepoFuncion>();
    builder.Services.AddScoped<IRepoLocal, RepoLocal>();
    builder.Services.AddScoped<IRepoOrden, RepoOrden>();
    builder.Services.AddScoped<IRepoSector, RepoSector>();
    builder.Services.AddScoped<IRepoTarifa, RepoTarifa>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

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