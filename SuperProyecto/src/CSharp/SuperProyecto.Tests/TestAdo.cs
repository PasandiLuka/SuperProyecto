using System.Data;
using Dapper;
using MySqlConnector;

public class TestAdo
{
    protected IDbConnection _conexion { get; private set; }

    public TestAdo()
    {
        /* string cadenaConexion = @"Server=localhost;Database=bd_test_boleteria;Uid=root;Pwd=48460731;"; */
        string cadenaConexion = @"Server=localhost;Database=bd_test_boleteria;Uid=5to_agbd;Pwd=Trigg3rs!;";
        /* string cadenaConexion = @"Server=localhost;Database=bd_boleteria;Uid=root;Pwd=root;"; */
        _conexion = new MySqlConnection(cadenaConexion);
        CrearBdDePrueba();
    }

    void CrearBdDePrueba()
    {
        /* string conection = @"Server=localhost;Uid=root;Pwd=48460731"; */
        string conection = @"Server=localhost;Uid=5to_agbd;Pwd=Trigg3rs!";

        string schemaDDL = Path.Combine(AppContext.BaseDirectory, "../../../../../../scripts/bd/MySQL/00 DDL.sql");
        string schemaINSERTS = Path.Combine(AppContext.BaseDirectory, "../../../../../../scripts/bd/MySQL/01 TESTINSERTS.sql");

        string schemaSql = File.ReadAllText(schemaDDL) + File.ReadAllText(schemaINSERTS);

        using (IDbConnection db = new MySqlConnection(conection))
        {
            db.Open();

            db.Execute("DROP DATABASE IF EXISTS bd_test_boleteria; CREATE DATABASE bd_test_boleteria CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;");

            db.Execute("USE bd_test_boleteria; " + schemaSql);
        }
    }
}