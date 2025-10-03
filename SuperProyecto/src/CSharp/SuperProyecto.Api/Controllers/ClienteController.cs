using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SuperProyecto.Core;
using SuperProyecto.Core.DTO;

namespace SuperProyecto.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ClienteController : ControllerBase
{
    readonly IRepoCliente _repoCliente;
    public ClienteController(IRepoCliente repoCliente)
    {
        _repoCliente = repoCliente;
    }

    [HttpGet]
    public IActionResult GetClientes()
    {
        var clientes = _repoCliente.GetClientes();
        return clientes.Any() ? Ok(clientes) : NoContent();
    }

    [HttpGet("{id}")]
    public IActionResult DetalleCliente(int id)
    {
        var cliente = _repoCliente.DetalleCliente((int)id);
        return cliente is not null ? Ok(cliente) : NotFound();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateCliente(int id, [FromBody] ClienteDTO clienteDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var cliente = _repoCliente.DetalleCliente((int)id);
        if (cliente is null)
            return NotFound();
        var clienteUpdate = new Cliente
        {
            DNI = id,
            nombre = clienteDto.nombre,
            apellido = clienteDto.apellido,
            email = clienteDto.email,
            telefono = clienteDto.telefono
        };
        _repoCliente.UpdateCliente(clienteUpdate, (int)id);
        return Ok(clienteUpdate);
    }

    [HttpPost]
    public IActionResult AltaCliente([FromBody] Cliente clienteAlta)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        _repoCliente.AltaCliente(clienteAlta);
        return Created();
    }

    /* #region EndPointsCliente

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
 */

}