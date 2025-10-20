using SuperProyecto.Core.Entidades;
using SuperProyecto.Core;
using SuperProyecto.Core.IServices;
using SuperProyecto.Services.Service;
using Moq;
using MySqlConnector;



namespace SuperProyecto.Tests;

public class TestAdoCliente : TestAdo
{

    [Fact]
    public void Cuando_se_agrega_un_nuevo_Cliente_se_Debe_Insertar_En_la_BD()
    {
        var moq = new Mock<IClienteService>();



    }
    [Fact]
    public void Lista_De_Clientes()
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


    [Fact]
    public void Mostrar_Detalle_Del_Cliente_Por_Id()
    {
        var moq = new Mock<IClienteService>();
        var id = 1;
        var cliente = new List<Cliente>
        {
            new Cliente {id = 1,DNI = 1, idUsuario = 1, nombre = "Juan", apellido = "Luna", telefono = 11342},
            new Cliente {id = 2, DNI = 2, idUsuario = 1, nombre = "Maria", apellido = "Escobar", telefono = 112832}
        };

        moq.Setup(r => r.DetalleCliente(id)).Returns(cliente);

        var resultado = moq.Object.DetalleCliente(DNI);

        Assert.NotNull(resultado); 
        Assert.Equal(2, ((List<Cliente>)resultado).Count);
    }


    [Fact]
    public void Cuando_se_agrega_un_nuevo_cliente_se_crea_nuevos_valores()
    {
        var moq = new Mock<IRepoCliente>();
        int DNI = 2;
        int idUsuario = 2;
        string nombre = "Carlos";
        string apellido = "Perez";
        int telefono = 123456;
        var cliente = new Cliente { DNI = DNI, idUsuario = idUsuario, nombre = nombre, apellido = apellido, telefono = telefono };

        moq.Object.AltaCliente(cliente);

        moq.Verify(r => r.AltaCliente(It.Is<Cliente>(t =>
            t.DNI == DNI &&
            t.idUsuario == idUsuario &&
            t.nombre == nombre &&
            t.apellido == apellido &&
            t.telefono == telefono
        )), Times.Once);
    }
}   


    



    

    

  
