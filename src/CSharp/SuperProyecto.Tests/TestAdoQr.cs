namespace SuperProyecto.Tests;

public class TestAdoQr
{
    [Fact]
    public void CuandoGeneroLaUrlDeUnQr_DebeRetornarUnaUrlCorrespondienteAlIdDeEntrada()
    {
        //Arrange
        var mockQrService = new Mock<IQrService>();
        var idEntrada = 10;
        var urlEsperada = "https://miapp.com/qr/10";

        mockQrService.Setup(s => s.GenerarQrUrl(idEntrada)).Returns(urlEsperada);

        //Act
        var resultado = mockQrService.Object.GenerarQrUrl(idEntrada);

        //Assert
        Assert.Equal(urlEsperada, resultado);
    }

    [Fact]
    public void CuandoCreoUnQr_DebeRetornarUnArregloDeBytesCorrespondienteALaUrl()
    {
        //Arrange
        var mockQrService = new Mock<IQrService>();
        var url = "https://miapp.com/qr/15";
        var qrBytes = new byte[] { 1, 2, 3, 4 };

        mockQrService.Setup(s => s.CrearQR(url)).Returns(qrBytes);

        //Act
        var resultado = mockQrService.Object.CrearQR(url);

        //Assert
        Assert.Equal(qrBytes, resultado);
    }
}