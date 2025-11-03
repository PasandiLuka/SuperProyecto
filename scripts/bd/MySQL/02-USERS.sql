SET autocommit=0;
START TRANSACTION;

DROP USER IF EXISTS 'administrador'@'localhost';
CREATE USER IF NOT EXISTS 'administrador'@'localhost' IDENTIFIED BY 'Trigg3rs!';
GRANT ALL ON bd_boleteria.* TO 'administrador'@'localhost';



DROP USER IF EXISTS 'cliente'@'localhost';
CREATE USER IF NOT EXISTS 'cliente'@'localhost' IDENTIFIED BY 'Trigg3rs!';
GRANT SELECT ON bd_boleteria.Usuario TO 'cliente'@'localhost';
GRANT SELECT, INSERT, UPDATE ON bd_boleteria.Cliente TO 'cliente'@'localhost';
GRANT SELECT ON bd_boleteria.Evento TO 'cliente'@'localhost';
GRANT SELECT ON bd_boleteria.Local TO 'cliente'@'localhost';
GRANT SELECT, UPDATE ON bd_boleteria.Tarifa TO 'cliente'@'localhost';
GRANT SELECT ON bd_boleteria.Funcion TO 'cliente'@'localhost';
GRANT SELECT ON bd_boleteria.Sector TO 'cliente'@'localhost';
GRANT SELECT, UPDATE, INSERT ON bd_boleteria.Orden TO 'cliente'@'localhost';
GRANT SELECT, INSERT ON bd_boleteria.Entrada TO 'cliente'@'localhost';
GRANT SELECT, INSERT ON bd_boleteria.Qr TO 'cliente'@'localhost';



DROP USER IF EXISTS 'organizador'@'localhost';
CREATE USER IF NOT EXISTS 'organizador'@'localhost' IDENTIFIED BY 'Trigg3rs!';
GRANT SELECT, UPDATE, INSERT ON bd_boleteria.Evento TO 'organizador'@'localhost';
GRANT SELECT, UPDATE, INSERT, DELETE ON bd_boleteria.Local TO 'organizador'@'localhost';
GRANT SELECT, UPDATE, INSERT ON bd_boleteria.Sector TO 'organizador'@'localhost';
GRANT SELECT, UPDATE, INSERT ON bd_boleteria.Funcion TO 'organizador'@'localhost';
GRANT SELECT, UPDATE, INSERT ON bd_boleteria.Tarifa TO 'organizador'@'localhost';
GRANT SELECT, UPDATE ON bd_boleteria.Entrada TO 'organizador'@'localhost';



DROP USER IF EXISTS 'default'@'localhost';
CREATE USER IF NOT EXISTS 'default'@'localhost' IDENTIFIED BY 'Trigg3rs!';
GRANT INSERT, SELECT ON bd_boleteria.Usuario TO 'default'@'localhost';
GRANT SELECT, INSERT, UPDATE ON bd_boleteria.RefreshTokens TO 'default'@'localhost';

COMMIT;