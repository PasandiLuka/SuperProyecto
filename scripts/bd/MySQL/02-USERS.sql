SET autocommit=0;
START TRANSACTION;

DROP USER IF EXISTS 'administrador'@'localhost';
CREATE USER IF NOT EXISTS 'administrador'@'localhost' IDENTIFIED BY 'Trigg3rs!';
GRANT SELECT, INSERT, UPDATE, DELETE ON 5to_boleteria.Cliente TO 'administrador'@'localhost';
GRANT SELECT, INSERT, UPDATE, DELETE ON 5to_boleteria.Entrada TO 'administrador'@'localhost';
GRANT SELECT, INSERT, UPDATE, DELETE ON 5to_boleteria.Evento TO 'administrador'@'localhost';
GRANT SELECT, INSERT, UPDATE, DELETE ON 5to_boleteria.Funcion TO 'administrador'@'localhost';
GRANT SELECT, INSERT, UPDATE, DELETE ON 5to_boleteria.Local TO 'administrador'@'localhost';
GRANT SELECT, INSERT, UPDATE, DELETE ON 5to_boleteria.Orden TO 'administrador'@'localhost';
GRANT SELECT, INSERT, UPDATE, DELETE ON 5to_boleteria.Qr TO 'administrador'@'localhost';
GRANT SELECT, INSERT, UPDATE, DELETE ON 5to_boleteria.RefreshTokens TO 'administrador'@'localhost';
GRANT SELECT, INSERT, UPDATE, DELETE ON 5to_boleteria.Sector TO 'administrador'@'localhost';
GRANT SELECT, INSERT, UPDATE, DELETE ON 5to_boleteria.Tarifa TO 'administrador'@'localhost';
GRANT SELECT, INSERT, UPDATE, DELETE ON 5to_boleteria.Usuario TO 'administrador'@'localhost';



DROP USER IF EXISTS 'cliente'@'localhost';
CREATE USER IF NOT EXISTS 'cliente'@'localhost' IDENTIFIED BY 'Trigg3rs!';
GRANT SELECT ON 5to_boleteria.Usuario TO 'cliente'@'localhost';
GRANT SELECT, INSERT, UPDATE ON 5to_boleteria.Cliente TO 'cliente'@'localhost';
GRANT SELECT ON 5to_boleteria.Evento TO 'cliente'@'localhost';
GRANT SELECT ON 5to_boleteria.Local TO 'cliente'@'localhost';
GRANT SELECT, UPDATE ON 5to_boleteria.Tarifa TO 'cliente'@'localhost';
GRANT SELECT ON 5to_boleteria.Funcion TO 'cliente'@'localhost';
GRANT SELECT ON 5to_boleteria.Sector TO 'cliente'@'localhost';
GRANT SELECT, UPDATE, INSERT ON 5to_boleteria.Orden TO 'cliente'@'localhost';
GRANT SELECT, INSERT ON 5to_boleteria.Entrada TO 'cliente'@'localhost';
GRANT SELECT, INSERT ON 5to_boleteria.Qr TO 'cliente'@'localhost';



DROP USER IF EXISTS 'organizador'@'localhost';
CREATE USER IF NOT EXISTS 'organizador'@'localhost' IDENTIFIED BY 'Trigg3rs!';
GRANT SELECT ON 5to_boleteria.Usuario TO 'organizador'@'localhost';
GRANT SELECT, UPDATE, INSERT ON 5to_boleteria.Evento TO 'organizador'@'localhost';
GRANT SELECT, UPDATE, INSERT, DELETE ON 5to_boleteria.Local TO 'organizador'@'localhost';
GRANT SELECT, UPDATE, INSERT ON 5to_boleteria.Sector TO 'organizador'@'localhost';
GRANT SELECT, UPDATE, INSERT ON 5to_boleteria.Funcion TO 'organizador'@'localhost';
GRANT SELECT, UPDATE, INSERT ON 5to_boleteria.Tarifa TO 'organizador'@'localhost';
GRANT SELECT, UPDATE ON 5to_boleteria.Entrada TO 'organizador'@'localhost';



DROP USER IF EXISTS 'default'@'localhost';
CREATE USER IF NOT EXISTS 'default'@'localhost' IDENTIFIED BY 'Trigg3rs!';
GRANT INSERT, SELECT ON 5to_boleteria.Usuario TO 'default'@'localhost';
GRANT SELECT, INSERT, UPDATE ON 5to_boleteria.RefreshTokens TO 'default'@'localhost';

COMMIT;