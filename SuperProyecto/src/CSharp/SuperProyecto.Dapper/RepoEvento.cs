using System.Data;
using Dapper;

using SuperProyecto.Core.Services.Persistencia;
using SuperProyecto.Core;

namespace SuperProyecto.Dapper;

public class RepoEvento : Repo, IRepoEvento
{
    /* public RepoEvento(IDbConnection conexion) : base(conexion) { }
    public RepoEvento(string conexion) : base(conexion) { } */

    public RepoEvento(IAdo _ado) : base(_ado) { }
    
    private static readonly string _queryEventos
        = @"SELECT * FROM Evento";
    public IEnumerable<Evento> GetEventos() => _conexion.Query<Evento>(_queryEventos);

    private static readonly string _queryDetalleEvento
        = @"SELECT * FROM Evento WHERE idEvento = @unIdEvento";
    public Evento? DetalleEvento(int idEvento) => _conexion.QueryFirstOrDefault<Evento>(_queryDetalleEvento, new {unIdEvento = idEvento});

    private static readonly string _queryAltaEvento
        = @"INSERT INTO Evento (idLocal, fechaIncio, descripcion) VALUES (@unIdLocal, @unaFechaInicio, @unaDescripcion)";
    public void AltaEvento(Evento evento) => _conexion.Execute(_queryAltaEvento, new { unIdLocal = evento.idLocal, unaFechaInicio = evento.fechaInicio, unaDescripcion = evento.descripcion });

    private static readonly string _queryUpdateEvento
        = @"UPDATE Evento SET idLocal = @unIdLocal, fechaInicio = @unaFechaInicio, descripcion = @unaDescripcion WHERE idEvento = @unIdEvento";
    public void UpdateEvento(Evento evento, int id) => _conexion.Execute(_queryUpdateEvento, new { unIdEvento = id, unIdLocal = evento.idLocal, unaFechaInicio = evento.fechaInicio, unaDescripcion = evento.descripcion });
}