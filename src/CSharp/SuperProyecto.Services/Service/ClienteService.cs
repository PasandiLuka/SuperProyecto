using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.IServices;
using SuperProyecto.Core.DTO;
using SuperProyecto.Services.Validators;

namespace SuperProyecto.Services.Service;

public class ClienteService : IClienteService
{
    readonly IRepoCliente _repoCliente;
    readonly ClienteValidator _validador;
    public ClienteService(ClienteValidator clienteValidator, IRepoCliente repoCliente)
    {
        _validador = clienteValidator;
        _repoCliente = repoCliente;
    }

    public Result<IEnumerable<Cliente>> GetClientes() => Result<IEnumerable<Cliente>>.Ok(_repoCliente.GetClientes());

    public Result<Cliente?> DetalleCliente(int id)
    {
        var cliente = _repoCliente.DetalleCliente(id);
        if (cliente is null) return Result<Cliente?>.NotFound("El cliente solicitado no fue encontrado.");
        return Result<Cliente>.Ok(cliente);
    } 

    public Result<Cliente> AltaCliente(ClienteDto clienteDto)
    {
        var resultado = _validador.Validate(clienteDto);
        if (!resultado.IsValid)
        {
            var listaErrores = resultado.Errors
                .GroupBy(a => a.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );
            return Result<Cliente>.BadRequest(listaErrores);
        }
        Cliente cliente = ConvertirDtoClase(clienteDto);
        return Result<Cliente>.Created(cliente);
    }

    public Result<Cliente> UpdateCliente(ClienteDto clienteDto, int id)
    {
        var resultado = _validador.Validate(clienteDto);
        if (!resultado.IsValid)
        {
            var listaErrores = resultado.Errors
                .GroupBy(a => a.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );
            return Result<Cliente>.BadRequest(listaErrores);
        }
        if(_repoCliente.DetalleCliente(id) is null) return Result<Cliente>.NotFound("El cliente a modificar no fue encontrado.");
        Cliente cliente = ConvertirDtoClase(clienteDto);
        _repoCliente.UpdateCliente(cliente, id);
        return Result<Cliente>.Ok(cliente);
    }

    Cliente ConvertirDtoClase(ClienteDto clienteDto)
    {
        return new Cliente
        {
            DNI = clienteDto.DNI,
            idUsuario = clienteDto.idUsuario,
            nombre = clienteDto.nombre,
            apellido = clienteDto.apellido,
            telefono = clienteDto.telefono
        };
    }
}