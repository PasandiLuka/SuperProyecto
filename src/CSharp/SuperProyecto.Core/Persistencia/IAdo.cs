using System.Data;

namespace SuperProyecto.Core.Persistencia;

public interface IAdo
{
    IDbConnection GetDbConnection();
}