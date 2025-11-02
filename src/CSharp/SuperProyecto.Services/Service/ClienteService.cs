using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.IServices;
using SuperProyecto.Core.DTO;
using SuperProyecto.Services.Validators;
using MySqlConnector;
namespace SuperProyecto.Services.Service;

public class ClienteService : IClienteService
{
    readonly IRepoCliente _repoCliente;
    readonly ClienteDtoUpdateValidator _validadorDto;
    readonly ClienteDtoAltaValidator _validador;
    public ClienteService(ClienteDtoUpdateValidator clienteDtoValidator, ClienteDtoAltaValidator clienteValidador, IRepoCliente repoCliente)
    {
        _validador = clienteValidador;
        _validadorDto = clienteDtoValidator;
        _repoCliente = repoCliente;
    }

    public Result<IEnumerable<ClienteResponse>> GetClientes()
    {
        try
        {
            return Result<IEnumerable<ClienteResponse>>.Ok(_repoCliente.GetClientes());
        }
        catch (MySqlException)
        {
            return Result<IEnumerable<ClienteResponse>>.Unauthorized();
        }
    }

    public Result<ClienteResponse?> DetalleCliente(int id)
    {
        try
        {
            var cliente = _repoCliente.DetalleCliente(id);
            if (cliente is null) return Result<ClienteResponse?>.NotFound("El cliente solicitado no fue encontrado.");
            return Result<ClienteResponse>.Ok(ConvertirClienteAResponse(cliente));
        }
        catch (MySqlException)
        {
            return Result<ClienteResponse?>.Unauthorized();
        }
    } 

    public Result<ClienteResponse> AltaCliente(ClienteDtoAlta clienteDtoAlta)
    {
        try
        {
            var resultado = _validador.Validate(clienteDtoAlta);
            if (!resultado.IsValid)
            {
                var listaErrores = resultado.Errors
                    .GroupBy(a => a.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    );
                return Result<ClienteResponse>.BadRequest(listaErrores);
            }
            Cliente cliente = new Cliente
            {
                DNI = clienteDtoAlta.DNI,
                idUsuario = clienteDtoAlta.idUsuario,
                nombre = clienteDtoAlta.nombre,
                apellido = clienteDtoAlta.apellido,
                telefono = clienteDtoAlta.telefono
            };
            _repoCliente.AltaCliente(cliente);
            return Result<ClienteResponse>.Created(ConvertirClienteAResponse(cliente)); 
        }
        catch (MySqlException)
        {
            return Result<ClienteResponse>.Unauthorized();
        }
    }

    public Result<ClienteResponse> UpdateCliente(ClienteDtoUpdate clienteDto, int id)
    {
        try
        {
            var resultado = _validadorDto.Validate(clienteDto);
            if (!resultado.IsValid)
            {
                var listaErrores = resultado.Errors
                    .GroupBy(a => a.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    );
                return Result<ClienteResponse>.BadRequest(listaErrores);
            }
            if (_repoCliente.DetalleCliente(id) is null) return Result<ClienteResponse>.NotFound("El cliente a modificar no fue encontrado.");
            Cliente cliente = new Cliente
            {
                nombre = clienteDto.nombre,
                apellido = clienteDto.apellido,
                telefono = clienteDto.telefono
            };
            _repoCliente.UpdateCliente(cliente, id);
            return Result<ClienteResponse>.Ok(ConvertirClienteAResponse(cliente));
        }
        catch (MySqlException)
        {
            return Result<ClienteResponse>.Unauthorized();
        }
    }
    
    private static ClienteResponse ConvertirClienteAResponse(Cliente cliente)
    {
        return new ClienteResponse
        {
            DNI = cliente.DNI,
            idUsuario = cliente.idUsuario,
            nombre = cliente.nombre,
            apellido = cliente.apellido,
            telefono = cliente.telefono
        };
    }
}