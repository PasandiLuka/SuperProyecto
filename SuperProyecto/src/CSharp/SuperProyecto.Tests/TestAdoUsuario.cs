using Moq;
using SuperProyecto.Core;
using SuperProyecto.Core.Persistencia;
using SuperProyecto.Dapper;
using MySqlConnector;
using SuperProyecto.Core;

namespace SuperProyecto.Tests;

public class TestAdoUsuario
{

    [Fact]
    public void CuandoHaceUnInsertEnTarifa_DebeAlmacenarDichaFilaEnLaTablaTarifa1()
        {
            var moq = new Mock<IRepoUsuario>();

        Usuario usuario = new Usuario { idUsuario = 100, email="si@gmail.com", passwordHash="siotravez" };

            moq.Setup(t => t.AltaUsuario(usuario));
            moq.Setup(t => t.DetalleUsuario(usuario.idUsuario)).Returns(usuario);
            var resultado = moq.Object.DetalleUsuario(usuario.idUsuario);

            Assert.NotNull(resultado);
            Assert.Equal(usuario.idUsuario, resultado.idUsuario);
        }
            
}
