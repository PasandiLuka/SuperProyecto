using System.Data;
using Dapper;

using SuperProyecto.Core.Services.Persistencia;
using SuperProyecto.Core;

namespace SuperProyecto.Dapper;

public class RepoEvento : Repo, IRepoEvento
{
    public RepoEvento(IAdo _ado) : base(_ado) { }
    
    private static readonly string _queryEventos
        = @"SELECT * FROM Evento";
    public IEnumerable<Evento> GetEventos() => _conexion.Query<Evento>(_queryEventos);

    private static readonly string _queryDetalleEvento
        = @"SELECT * FROM Evento WHERE idEvento = @unIdEvento";
    public Evento? DetalleEvento(int idEvento) => _conexion.QueryFirstOrDefault<Evento>(_queryDetalleEvento, new {unIdEvento = idEvento});

    private static readonly string _queryAltaEvento
        = @"INSERT INTO Evento (nombre, descripcion, fechaPublicacion, publicado) VALUES (@nombre, @descripcion, @fechaPublicacion, @publicado)";
    public void AltaEvento(Evento evento)
    {
        _conexion.Execute(
            _queryAltaEvento,
            new
            {
                evento.nombre,
                evento.descripcion,
                evento.fechaPublicacion,
                evento.publicado
            });
    }

    private static readonly string _queryUpdateEvento
        = @"UPDATE Evento SET nombre = @nombre, descripcion = @descripcion, fechaPublicacion = @fechaPublicacion, publicado = @publicado WHERE idEvento = @idEvento";
    public void UpdateEvento(Evento evento, int id)
    {
        _conexion.Execute(
            _queryUpdateEvento,
            new
            {
                evento.nombre,
                evento.descripcion,
                evento.fechaPublicacion,
                evento.publicado,
                idEvento = id
            });
    }
}