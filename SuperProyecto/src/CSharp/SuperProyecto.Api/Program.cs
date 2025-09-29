using SuperProyecto.Dapper;
using SuperProyecto.Tests;
using SuperProyecto.Core;

TestAdo _testAdo = new TestAdo();

RepoCliente _repoCliente = new RepoCliente(_testAdo._conexion);

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

////////


app.MapGet("api/clientes/{id:int}", (int? id) => _repoCliente.DetalleCliente((int)id) is null ? Results.NotFound() : Results.Ok(_repoCliente.DetalleCliente((int)id)));

app.MapGet("api/clientes", () => Results.Ok(_repoCliente.GetClientes()));

////////

app.Run();