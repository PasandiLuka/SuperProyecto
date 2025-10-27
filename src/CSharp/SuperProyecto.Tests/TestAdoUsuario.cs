using Moq;
using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.IServices;
using SuperProyecto.Services.Service;
using MySqlConnector;
using SuperProyecto.Core;
using SuperProyecto.Core.Persistencia;
using SuperProyecto.Core.DTO;


namespace SuperProyecto.Tests;

public class TestAdoUsuario
{

    [Fact]
    public void CuandoHaceUnInsertEnTarifa_DebeAlmacenarDichaFilaEnLaTablaTarifa1()
        {
            var moq = new Mock<IUsuarioService>();

        UsuarioDto usuario = new UsuarioDto {  email="si@gmail.com", password="siotravez" };

            moq.Setup(t => t.AltaUsuario(usuario));
            var resultado = moq.Object.DetalleUsuario(usuario.id);

            Assert.NotNull(resultado);
            Assert.Equal(id, resultado.idUsuario);
        }

        
    [Fact]
    public void Cuando_se_agrega_una_nueva_usuario_se_crea_nuevos_valores_de_las_variables()
    {
        
        var moq = new Mock<IUsuarioService>();
        string email = "Si@gmail.com";
        string passwordHash = "contraseña";
        var usuario = new UsuarioDto { email = email, password = passwordHash };

        
        moq.Object.AltaUsuario(usuario);

        
        moq.Verify(r => r.AltaUsuario(It.Is<UsuarioDto>(t =>
            t.email == email &&
            t.password == passwordHash
        )), Times.Once);
    }
}
