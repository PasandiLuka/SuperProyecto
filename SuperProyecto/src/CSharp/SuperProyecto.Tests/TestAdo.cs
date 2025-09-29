using System.Data;
using MySqlConnector;

public class TestAdo
{
    protected IDbConnection _conexion { get; private set; }

    public TestAdo()
    {
        // string cadenaConexion = @"Server=localhost;Database=bd_boleteria;Uid=5to_agbd;Pwd=Trigg3rs!;";
        string cadenaConexion = @"Server=localhost;Database=bd_boleteria;Uid=root;Pwd=root;";
        _conexion = new MySqlConnection(cadenaConexion);
    }
}