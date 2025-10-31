namespace SuperProyecto.Core.IServices;

public interface IDataBaseConnectionService
{
    string GetConnectionString(bool isRoot);
}