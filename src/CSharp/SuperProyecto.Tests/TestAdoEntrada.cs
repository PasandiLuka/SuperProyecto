// using SuperProyecto.Core.Entidades;
// using SuperProyecto.Core;
// using SuperProyecto.Core.IServices;
// using SuperProyecto.Services.Service;
// using Moq;
// using MySqlConnector;

// namespace SuperProyecto.Tests;

// public class TestAdoEntrada
// {

//     //Lista entradas
//     [Fact]
//     public void Retornar_Lista_De_Entradas()
//     {
//         var moq = new Mock<IEntradaService>();
//         List<Entrada> entrada = new List<Entrada>
//         {
//             new Entrada{idEntrada = 1, idOrden = 1, idQR = 12345,usada = true},
//             new Entrada{idEntrada = 2, idOrden = 2, idQR = 67890,usada = true}
//         };

//         moq.Setup(c => c.GetEntradas()).Returns(entrada);
//         var resultado = moq.Object.GetEntradas();

//         Assert.NotEmpty(resultado);
//         Assert.Equal(2, ((List<Entrada>)resultado).Count());
//     }

//     //Detalle de una entrada
//     [Fact]
//     public void Retornar_Detalle_Del_Local_Por_Id()
//     {
//         var moq = new Mock<IEntradaService>();
//         var id = 1;
//         var entrada = new Entrada { idEntrada = 1, idOrden = 1, idQR = 12345,usada = true};

//         moq.Setup(c => c.DetalleEntrada(id)).Returns(entrada);
//         var resultado = moq.Object.DetalleEntrada(id);

//         Assert.NotNull(resultado);
//         Assert.Equal(entrada.idEntrada, resultado.idLocal);
//         Assert.Equal(entrada.idOrden, resultado.idOrden);
//         Assert.Equal(entrada.idQR, resultado.idQR);
//         Assert.Equal(entrada.usada, resultado.usada);
//     }
    
//     //Anula la entrada
    
// }