SET autocommit=0;
START TRANSACTION;

CREATE USER IF NOT EXISTS "administrador"@"localhost" IDENTIFIED BY "123456";
GRANT ALL ON bd_boleteria.* TO "administrador"@"localhost";



CREATE USER IF NOT EXISTS "cliente"@"localhost" IDENTIFIED BY "123456";
GRANT SELECT, INSERT, UPDATE ON bd_boleteria.Cliente TO "cliente"@"localhost";



CREATE USER IF NOT EXISTS "organizador"@"localhost" IDENTIFIED BY "123456";
GRANT SELECT, INSERT, UPDATE ON bd_boleteria.Local TO "organizador"@"localhost";

COMMIT;