using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.IServices;
using SuperProyecto.Core.DTO;
using SuperProyecto.Services.Validators;
using FluentValidation;

namespace SuperProyecto.Services.Service;

public class ClienteService : IClienteService
{
    readonly IRepoCliente _repoCliente;
    readonly ClienteValidator _clienteValidator;
    public ClienteService(ClienteValidator clienteValidator, IRepoCliente repoCliente)
    {
        _clienteValidator = clienteValidator;
        _repoCliente = repoCliente;
    }

    public IEnumerable<Cliente> GetClientes() => _repoCliente.GetClientes();

    public Cliente? DetalleCliente(int id) => _repoCliente.DetalleCliente(id);

    public void AltaCliente(ClienteDto clienteDto)
    {
        Cliente cliente = ConvertirDtoClase(clienteDto);
        _repoCliente.AltaCliente(cliente);
    }

    public void UpdateCliente(ClienteDto clienteDto, int id)
    {
        Cliente cliente = ConvertirDtoClase(clienteDto);
        _repoCliente.UpdateCliente(cliente, id);  
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