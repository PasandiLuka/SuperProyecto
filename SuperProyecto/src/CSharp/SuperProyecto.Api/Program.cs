using SuperProyecto.Dapper;
using SuperProyecto.Tests;
using SuperProyecto.Core.Services.Persistencia;
using SuperProyecto.Core;
using SuperProyecto.Core.Services.Validators;
using Xunit.Sdk;


TestAdo _testAdo = new TestAdo();

IRepoCliente _repoCliente = new RepoCliente(_testAdo._conexion);
IRepoEntrada _repoEntrada = new RepoEntrada(_testAdo._conexion);
IRepoEvento _repoEvento = new RepoEvento(_testAdo._conexion);
IRepoFuncion _repoFuncion = new RepoFuncion(_testAdo._conexion);
IRepoLocal _repoLocal = new RepoLocal(_testAdo._conexion);
IRepoOrden _repoOrden = new RepoOrden(_testAdo._conexion);
IRepoSector _repoSector = new RepoSector(_testAdo._conexion);
IRepoTarifa _repoTarifa = new RepoTarifa(_testAdo._conexion);

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

app.MapGet("api/cliente", () =>
{
    var clientes = _repoCliente.GetClientes();
    return clientes.Any() ? Results.Ok(clientes) : Results.NoContent();
}
);

app.MapGet("api/cliente/{id:int}", (int? id) =>
{
    var cliente = _repoCliente.DetalleCliente((int)id);
    return cliente is not null ? Results.Ok(cliente) : Results.NotFound();
});

app.MapPut("api/cliente/{id:int}", (int? id, Cliente clienteUpdate) =>
{
    var validator = new ClienteValidator();
    var result = validator.Validate(clienteUpdate);
    if(!result.IsValid)
    {
        var listaErrores = result.Errors
            .GroupBy(a => a.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(e => e.ErrorMessage).ToArray()
            );
        
        return Results.ValidationProblem(listaErrores);
    }

    var cliente = _repoCliente.DetalleCliente((int)id);
    if(cliente is null)
        return Results.NotFound();
    
    _repoCliente.UpdateCliente(clienteUpdate, (int)id);
    return Results.Ok(clienteUpdate);
});

#endregion


#region EndPointsEntrada
app.MapGet("api/entrada", () =>
{
    var entradas = _repoEntrada.GetEntradas();
    return entradas.Any() ? Results.Ok(entradas) : Results.NoContent();
});

app.MapGet("api/entrada/{id:int}", (int? id) =>
{
    var entrada = _repoEntrada.DetalleEntrada((int)id);
    return entrada is not null ?  Results.Ok(entrada) : Results.NotFound();
});

app.MapPut("api/entrada/{id:int}", (int? id, Entrada entradaUpdate) =>
{
    var entrada = _repoEntrada.DetalleEntrada((int)id);
    if(entrada is null)
        return Results.NotFound();
    
    _repoEntrada.UpdateEntrada(entradaUpdate, (int)id);
    return Results.Ok(entradaUpdate);
});
#endregion


#region EndPointsEvento
app.MapGet("api/evento", () =>
{
    var eventos = _repoEvento.GetEventos();
    return eventos.Any() ? Results.Ok(eventos) : Results.NoContent();
});

app.MapGet("api/evento/{id:int}", (int? id) =>
{
    var evento = _repoEvento.DetalleEvento((int)id);
    return evento is not null ?  Results.Ok(evento) : Results.NotFound();
});

app.MapPut("api/evento/{id:int}", (int? id, Evento eventoUpdate) =>
{
    var evento = _repoEvento.DetalleEvento((int)id);
    if(evento is null)
        return Results.NotFound();
    
    _repoEvento.UpdateEvento(eventoUpdate, (int)id);
    return Results.Ok(eventoUpdate);
});
#endregion


#region EndPointsFuncion

app.MapGet("api/funcion", () =>
{
    var funciones = _repoFuncion.GetFunciones();
    return funciones.Any() ? Results.Ok(funciones) : Results.NoContent();
});

app.MapGet("api/funcion/{id:int}", (int? id) =>
{
    var funcion = _repoFuncion.DetalleFuncion((int)id);
    return funcion is not null ?  Results.Ok(funcion) : Results.NotFound();
});

app.MapPut("api/funcion/{id:int}", (int? id, Funcion funcionUpdate) =>
{
    var funcion = _repoFuncion.DetalleFuncion((int)id);
    if(funcion is null)
        return Results.NotFound();
    
    _repoFuncion.UpdateFuncion(funcionUpdate, (int)id);
    return Results.Ok(funcionUpdate);
});
#endregion


#region EndPointsLocal
app.MapGet("api/local", () =>
{
    var locales = _repoLocal.GetLocales();
    return locales.Any() ? Results.Ok(locales) : Results.NoContent();
});

app.MapGet("api/local/{id:int}", (int? id) =>
{
    var local = _repoLocal.DetalleLocal((int)id);
    return local is not null ?  Results.Ok(local) : Results.NotFound();
});

app.MapPut("api/local/{id:int}", (int? id, Local localUpdate) =>
{
    var local = _repoLocal.DetalleLocal((int)id);
    if(local is null)
        return Results.NotFound();
    
    _repoLocal.UpdateLocal(localUpdate, (int)id);
    return Results.Ok(localUpdate);
});
#endregion


#region EndPointsOrden
app.MapGet("api/orden", () =>
{
    var ordenes = _repoOrden.GetOrdenes();
    return ordenes.Any() ? Results.Ok(ordenes) : Results.NoContent();
});

app.MapGet("api/orden/{id:int}", (int? id) =>
{
    var orden = _repoOrden.DetalleOrden((int)id);
    return orden is not null ?  Results.Ok(orden) : Results.NotFound();
});

app.MapPut("api/orden/{id:int}", (int? id, Orden ordenUpdate) =>
{
    var orden = _repoOrden.DetalleOrden((int)id);
    if(orden is null)
        return Results.NotFound();
    
    _repoOrden.UpdateOrden(ordenUpdate, (int)id);
    return Results.Ok(ordenUpdate);
});
#endregion


#region EndPointsSector
app.MapGet("api/sector", () =>
{
    var sectores = _repoSector.GetSectores();
    return sectores.Any() ? Results.Ok(sectores) : Results.NoContent();
});

app.MapGet("api/sector/{id:int}", (int? id) =>
{
    var sector = _repoSector.DetalleSector((int)id);
    return sector is not null ?  Results.Ok(sector) : Results.NotFound();
});

app.MapPut("api/sector/{id:int}", (int? id, Sector sectorUpdate) =>
{
    var sector = _repoSector.DetalleSector((int)id);
    if(sector is null)
        return Results.NotFound();
    
    _repoSector.UpdateSector(sectorUpdate, (int)id);
    return Results.Ok(sectorUpdate);
});
#endregion


#region EndPointsTarifa
app.MapGet("api/tarifa", () =>
{
    var sectores = _repoSector.GetSectores();
    return sectores.Any() ? Results.Ok(sectores) : Results.NoContent();
});

app.MapGet("api/tarifa/{id:int}", (int? id) =>
{
    var tarifa = _repoSector.DetalleSector((int)id);
    return tarifa is not null ?  Results.Ok(tarifa) : Results.NotFound();
});

app.MapPut("api/tarifa/{id:int}", (int? id, Tarifa tarifaUpdate) =>
{
    var tarifa = _repoTarifa.DetalleTarifa((int)id);
    if(tarifa is null)
        return Results.NotFound();
    
    _repoTarifa.UpdateTarifa(tarifaUpdate, (int)id);
    return Results.Ok(tarifaUpdate);
});
#endregion
////////

app.Run();