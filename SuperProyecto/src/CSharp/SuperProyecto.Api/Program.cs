using System.Data;
using Swashbuckle.SwaggerUi;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

string conectionString = @"Server=localhost;Database=bd_boleteria;Uid=5to_agbd;Pwd=Trigg3rs!;";

#region ReposScoped
builder.Services.AddScoped<IAdo>(sp => new Ado(conectionString));
builder.Services.AddScoped<IRepoCliente, RepoCliente>();
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