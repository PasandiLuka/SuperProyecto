using System.Data;

namespace SuperProyecto.Core.Services.Persistencia;

public interface IAdo
{
    IDbConnection GetDbConnection();
}