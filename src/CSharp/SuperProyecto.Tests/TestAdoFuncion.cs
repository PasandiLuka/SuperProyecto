// using Moq;
// using SuperProyecto.Core.Entidades;
// using SuperProyecto.Core.IServices;
// using SuperProyecto.Services.Service;
// using MySqlConnector;
// using SuperProyecto.Core;

// namespace SuperProyecto.Tests;

// public class TestAdoFuncion : TestAdo
// {
//     private IRepoFuncion _repoFuncion;

//     public TestAdoFuncion()
//     {
//         _repoFuncion = new RepoFuncion(_conexion);
//     }

//     [Fact]
//     public void CuandoHaceUnInsertEnFuncion_DebeAlmacenarDichaFilaEnLaTablaFuncion()
//     {
//         var funcion = new Funcion()
//         {
//             idFuncion = 200,
//             idEvento = 1,
//             descripcion = "vale_por_una_descripcion",
//             fechaHora = DateTime.Parse("2024-12-31 20:00:00")
//         };

//         _repoFuncion.AltaFuncion(funcion);

//         var funcionDB = _repoFuncion.DetalleFuncion(200);

//         Assert.NotNull(funcionDB);
//         Assert.Equal(funcion.idFuncion, funcionDB.idFuncion);
//         Assert.Equal(funcion.idEvento, funcionDB.idEvento);
//         Assert.Equal(funcion.descripcion, funcionDB.descripcion);
//         Assert.Equal(funcion.fechaHora, funcionDB.fechaHora);
//     }

//     [Fact]
//     public void CuandoHagoUnInsertConUnaPKDuplicada_DebeTirarUnaExcepcion()
//     {
//         var funcion = new Funcion()
//         {
//             idFuncion = 201,
//             idEvento = 1,
//             descripcion = "vale_por_una_descripcion",
//             fechaHora = DateTime.Parse("2024-12-31 20:00:00")
//         };

//         _repoFuncion.AltaFuncion(funcion);

//         Assert.Throws<MySqlException>(() => _repoFuncion.AltaFuncion(funcion));
//     }

//     [Fact]
//     public void CuandoHagoUnUpdateEnLaTablaCliente_DebeHacerLasRespectivasModificaciones()
//     {
//         var _funcion = new Funcion()
//         {
//             idFuncion = 202,
//             idEvento = 1,
//             descripcion = "vale_por_una_descripcion",
//             fechaHora = DateTime.Parse("2024-12-31 20:00:00")
//         };
        
//         _repoFuncion.AltaFuncion(_funcion);

//         var _funcionUpdate = new Funcion()
//         {
//             idFuncion = 1,
//             idEvento = 1,
//             descripcion = "vale_por_una_descripcionUpdate",
//             fechaHora = DateTime.Parse("2025-12-31 20:00:00")
//         };

//         _repoFuncion.UpdateFuncion(_funcionUpdate, _funcion.idFuncion);

//         var _funcionDB = _repoFuncion.DetalleFuncion(_funcion.idFuncion);

//         Assert.NotNull(_funcionDB);
//         Assert.Equal(_funcionUpdate.idEvento, _funcionDB.idEvento);
//         Assert.Equal(_funcionUpdate.descripcion, _funcionDB.descripcion);
//         Assert.Equal(_funcionUpdate.fechaHora, _funcionDB.fechaHora);
//     }
// } */