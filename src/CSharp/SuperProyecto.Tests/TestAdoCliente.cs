using SuperProyecto.Core.Entidades;
using SuperProyecto.Core;
using SuperProyecto.Core.IServices;
using SuperProyecto.Services.Service;
using SuperProyecto.Core.DTO;
using Moq;
using MySqlConnector;
using System.Reflection;

namespace SuperProyecto.Tests;

public class TestAdoCliente 
{

    //Crear cliente
    [Fact]
    public void AltaCliente_DebeAlmacenarDichaFilaEnLaTablaCliente()
    {

        var mockService = new Mock<IClienteService>();
        var clienteDto = new ClienteDto { DNI = 12345678, idUsuario = 1, nombre = "Isac", apellido = "Hernandez", telefono = 13452 };

        ClienteDto? cliente = null;

        mockService.Setup(s => s.AltaCliente(It.IsAny<ClienteDto>())).Callback<ClienteDto>(c => cliente = c);

        mockService.Object.AltaCliente(clienteDto);

        Assert.NotNull(cliente);
        Assert.Equal(clienteDto.DNI, cliente.DNI);
        Assert.Equal(clienteDto.idUsuario, cliente.idUsuario);
        Assert.Equal(clienteDto.nombre, cliente.nombre);
        Assert.Equal(clienteDto.apellido, cliente.apellido);
        Assert.Equal(clienteDto.telefono, cliente.telefono);

        // var mockService = new Mock<IClienteService>();
        // var clienteDto = new ClienteDto{DNI = 12348, idUsuario = 1 ,nombre = "Victoria",apellido = "Gonzalez",telefono = 123789 };
        // mockService.Object.AltaCliente(clienteDto);

        // mockService.Verify(s => s.AltaCliente(It.Is<ClienteDto>(c =>
        //     c.DNI == clienteDto.DNI &&
        //     c.Nombre == clienteDto.Nombre &&
        //     c.Apellido == clienteDto.Apellido &&
        //     c.Telefono == clienteDto.Telefono
        // )), Times.Once);

    }

    //Lista de clientes
    [Fact]
    public void Retornar_Lista_De_Clientes()
    {
        var moq = new Mock<IClienteService>();
        List<Cliente> cliente = new List<Cliente>
        {
            new Cliente{DNI = 1, idUsuario = 1, nombre = "juan", apellido = "antonio", telefono = 1},
            new Cliente{DNI = 1, idUsuario = 1, nombre = "fede", apellido = "mesa", telefono = 1}
        };

        moq.Setup(c => c.GetClientes()).Returns(cliente);
        var resultado = moq.Object.GetClientes();

        Assert.NotEmpty(resultado);
        Assert.Equal(2, ((List<Cliente>)resultado).Count());
    }

    //Detalle de cliente
    [Fact]
    public void Retornar_Detalle_De_Cliente()
    {
        var moq = new Mock<IClienteService>();
        var id = 1;
        var cliente = new Cliente { DNI = 1, idUsuario = 1, nombre = "Lujan", apellido = "antonio", telefono = 1 };

        moq.Setup(c => c.DetalleCliente(id)).Returns(cliente);
        var resultado = moq.Object.DetalleCliente(id);

        Assert.NotNull(resultado);
        Assert.Equal(cliente.DNI, resultado.DNI);
        Assert.Equal(cliente.idUsuario, resultado.idUsuario);
        Assert.Equal(cliente.nombre, resultado.nombre);
        Assert.Equal(cliente.apellido, resultado.apellido);
        Assert.Equal(cliente.telefono, resultado.telefono);

        // var mockService = new Mock<IClienteService>();
        // var clienteId = 1;
        // var cliente = new Cliente{DNI = 12345678,idUsuario = 1,nombre = "Juan",apellido = "Pérez",telefono = 123456789 };
        // mockService.Setup(s => s.DetalleCliente(clienteId)).Returns(cliente);

        // var resultado = mockService.Object.DetalleCliente(clienteId);

        // Assert.NotNull(resultado);
        // Assert.Equal(cliente.DNI, resultado.DNI);
        // Assert.Equal(cliente.idUsuario, resultado.idUsuario);
        // Assert.Equal(cliente.nombre, resultado.nombre);
        // Assert.Equal(cliente.apellido, resultado.apellido);
        // Assert.Equal(cliente.telefono, resultado.telefono);
    }

    //Actualiza datos
    [Fact]
    public void Actualizar_Cliente_Debe_Modificar_Los_Datos_En_La_Tabla_Cliente()
    {
        var moq = new Mock<IClienteService>();
        // var mockService = new Mock<IClienteService>();
        // var clienteDto = new ClienteDto { DNI = 12345678, idUsuario = 1, nombre = "Isac", apellido = "Hernandez", telefono = 13452 };

        // ClienteDto? clienteActualizado = null;

        // mockService.Setup(s => s.ActualizarCliente(It.IsAny<ClienteDto>())).Callback<ClienteDto>(c => clienteActualizado = c);

        // mockService.Object.ActualizarCliente(clienteDto);

        // Assert.NotNull(clienteActualizado);
        // Assert.Equal(clienteDto.DNI, clienteActualizado.DNI);
        // Assert.Equal(clienteDto.Nombre, clienteActualizado.Nombre);
        // Assert.Equal(clienteDto.Apellido, clienteActualizado.Apellido);
        // Assert.Equal(clienteDto.Telefono, clienteActualizado.Telefono);
    }
}   




    

    

  
