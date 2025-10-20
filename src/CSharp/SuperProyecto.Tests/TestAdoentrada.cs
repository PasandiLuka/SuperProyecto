// using SuperProyecto.Core.Entidades;
// using SuperProyecto.Core.Persistencia;
// using SuperProyecto.Dapper;
// using MySqlConnector;
// using Moq;

// namespace SuperProyecto.Tests;

// public class TestAdoEntrada
// { 
//     [Fact]
//     public void CuandoHaceUnInsertenEntrada_DebeAlmacenarDichaFilaEnLaTablaEntrada()
//         {
//             var moq = new Mock<IRepoEntrada>();

//         Entrada entrada = new Entrada { idEntrada = 100 , idFuncion = 100, idOrden = 100 , idQr  = 100, usada = true};

//             moq.Setup(t => t.AltaEntrada(entrada));
//             moq.Setup(t => t.DetalleEntrada(entrada.idEntrada)).Returns(entrada);
//             var resultado = moq.Object.DetalleEntrada(entrada.idEntrada);

//             Assert.NotNull(resultado);
//             Assert.Equal(entrada.idEntrada, resultado.idEntrada);
//         }


            
//     [Fact]
//     public void CuandoSolicitaTarifasPorFuncion_DebeRetornarListaDeTarifas()
//     {
//         var moq = new Mock<IRepoEntrada>();
//         int idEntrada = 100;
//         var entradas = new List<Entrada>
//         {
//             new Entrada { idEntrada = 100 , idFuncion = 100, idOrden = 100 , idQr  = 100, usada = true},
//             new Entrada {idEntrada = 100 , idFuncion = 100, idOrden = 100 , idQr  = 100, usada = true }
//         };

//         moq.Setup(r => r.GetEntradas()).Returns(entradas);

//         var resultado = moq.Object.GetEntradas();

//         Assert.NotNull(resultado);
//         Assert.Equal(2,((List<Entrada>)resultado).Count);
//         Assert.All(resultado, e => Assert.Equal(idEntrada, e.idEntrada));
//     }
// }