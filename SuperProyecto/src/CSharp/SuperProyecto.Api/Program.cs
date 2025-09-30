using SuperProyecto.Dapper;
using SuperProyecto.Tests;
using SuperProyecto.Core;


TestAdo _testAdo = new TestAdo();

RepoCliente _repoCliente = new RepoCliente(_testAdo._conexion);
RepoFuncion _repoFuncion = new RepoFuncion(_testAdo._conexion);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

////////

#region EndPointsCliente

app.MapGet("api/cliente/{id:int}", (int? id) =>
{
    var cliente = _repoCliente.DetalleCliente((int)id);
    return cliente is not null ? Results.Ok(cliente) : Results.NotFound();
});

app.MapGet("api/cliente", () =>
{
    var clientes = _repoCliente.GetClientes();
    return clientes.Any() ? Results.Ok(clientes) : Results.NoContent();
}
);

#endregion

app.MapGet("api/funcion/{id:int}", (int? id) =>
{
    var funcion = _repoFuncion.DetalleFuncion((int)id);
    return funcion is not null ?  Results.Ok(funcion) : Results.NotFound();
});

app.MapGet("api/funcion", () =>
{
    var funciones = _repoFuncion.GetFunciones();
    return funciones.Any() ? Results.Ok(funciones) : Results.NoContent();
});

////////

app.Run();