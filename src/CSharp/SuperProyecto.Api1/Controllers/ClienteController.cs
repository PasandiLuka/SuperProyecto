using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SuperProyecto.Core;
using SuperProyecto.Core.DTO;
using Microsoft.AspNetCore.Authorization;

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

    [Authorize]
    [HttpGet]
    public IActionResult GetClientes()
    {
        var clientes = _repoCliente.GetClientes();
        return clientes.Any() ? Ok(clientes) : NoContent();
    }

    [Authorize]
    [HttpGet("{id}")]
    public IActionResult DetalleCliente(int id)
    {
        var cliente = _repoCliente.DetalleCliente((int)id);
        return cliente is not null ? Ok(cliente) : NotFound();
    }

    [Authorize(Roles = "Administrador, Cliente")]
    [HttpPut("{id}")]
    public IActionResult UpdateCliente(int id, [FromBody] ClienteDto clienteDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var cliente = _repoCliente.DetalleCliente((int)id);
        if (cliente is null) return NotFound();
        var clienteUpdate = new Cliente
        {
            DNI = id,
            idUsuario = cliente.idUsuario,
            nombre = clienteDto.nombre,
            apellido = clienteDto.apellido,
            telefono = clienteDto.telefono
        };
        _repoCliente.UpdateCliente(clienteUpdate, (int)id);
        return Ok(clienteUpdate);
    }

    [Authorize(Roles = "Administrador, Cliente")]
    [HttpPost]
    public IActionResult AltaCliente([FromBody] Cliente clienteAlta)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        _repoCliente.AltaCliente(clienteAlta);
        return Created();
    }
}