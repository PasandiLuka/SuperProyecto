using Moq;
using SuperProyecto.Core.Entidades;
using SuperProyecto.Core.IServices;
using SuperProyecto.Services.Service;
using MySqlConnector;
using SuperProyecto.Core;
using SuperProyecto.Core.Persistencia;


namespace SuperProyecto.Tests;

public class TestAdoUsuario
{

    [Fact]
    public void CuandoHaceUnInsertEnTarifa_DebeAlmacenarDichaFilaEnLaTablaTarifa1()
        {
            var moq = new Mock<IUsuarioService>();

        Usuario usuario = new Usuario { idUsuario = 100, email="si@gmail.com", passwordHash="siotravez" };

            moq.Setup(t => t.AltaUsuario(usuario));
            moq.Setup(t => t.DetalleUsuario(usuario.idUsuario)).Returns(usuario);
            var resultado = moq.Object.DetalleUsuario(usuario.idUsuario);

            Assert.NotNull(resultado);
            Assert.Equal(usuario.idUsuario, resultado.idUsuario);
        }

        
    [Fact]
    public void Cuando_se_agrega_una_nueva_usuario_se_crea_nuevos_valores_de_las_variables()
    {
        // Arrange
        var moq = new Mock<IUsuarioService>();
        int idUsuario = 10;
        string email = "Si@gmail.com";
        string passwordHash = "contraseña";
        var usuario = new Usuario { idUsuario = idUsuario, email = email, passwordHash = passwordHash };

        // Act
        moq.Object.AltaUsuario(usuario);

        // Assert
        moq.Verify(r => r.AltaUsuario(It.Is<Usuario>(t =>
            t.idUsuario == idUsuario &&
            t.email == email &&
            t.passwordHash == passwordHash
        )), Times.Once);
    }
}
