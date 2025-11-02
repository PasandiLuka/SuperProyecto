using SuperProyecto.Core.Enums;

namespace SuperProyecto.Core.IServices;

public interface IDataBaseConnectionService
{
    string GetConnectionRootString();
    string GetConnectionUserString(string rol);
}