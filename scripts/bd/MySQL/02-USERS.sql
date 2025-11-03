SET autocommit=0;
START TRANSACTION;

DROP USER IF EXISTS 'Administrador'@'localhost';
CREATE USER IF NOT EXISTS 'Administrador'@'localhost' IDENTIFIED BY 'Trigg3rs!';
GRANT ALL ON 5to_boleteria.* TO 'Administrador'@'localhost';



DROP USER IF EXISTS 'Cliente'@'localhost';
CREATE USER IF NOT EXISTS 'Cliente'@'localhost' IDENTIFIED BY 'Trigg3rs!';
GRANT SELECT ON 5to_boleteria.Usuario TO 'Cliente'@'localhost';
GRANT SELECT, INSERT, UPDATE ON 5to_boleteria.Cliente TO 'Cliente'@'localhost';
GRANT SELECT ON 5to_boleteria.Evento TO 'Cliente'@'localhost';
GRANT SELECT ON 5to_boleteria.Local TO 'Cliente'@'localhost';
GRANT SELECT, UPDATE ON 5to_boleteria.Tarifa TO 'Cliente'@'localhost';
GRANT SELECT ON 5to_boleteria.Funcion TO 'Cliente'@'localhost';
GRANT SELECT ON 5to_boleteria.Sector TO 'Cliente'@'localhost';
GRANT SELECT, UPDATE, INSERT ON 5to_boleteria.Orden TO 'Cliente'@'localhost';
GRANT SELECT, INSERT ON 5to_boleteria.Entrada TO 'Cliente'@'localhost';
GRANT SELECT, INSERT ON 5to_boleteria.Qr TO 'Cliente'@'localhost';



DROP USER IF EXISTS 'Organizador'@'localhost';
CREATE USER IF NOT EXISTS 'Organizador'@'localhost' IDENTIFIED BY 'Trigg3rs!';
GRANT SELECT, UPDATE, INSERT ON 5to_boleteria.Evento TO 'Organizador'@'localhost';
GRANT SELECT, UPDATE, INSERT, DELETE ON 5to_boleteria.Local TO 'Organizador'@'localhost';
GRANT SELECT, UPDATE, INSERT ON 5to_boleteria.Sector TO 'Organizador'@'localhost';
GRANT SELECT, UPDATE, INSERT ON 5to_boleteria.Funcion TO 'Organizador'@'localhost';
GRANT SELECT, UPDATE, INSERT ON 5to_boleteria.Tarifa TO 'Organizador'@'localhost';
GRANT SELECT, UPDATE ON 5to_boleteria.Entrada TO 'Organizador'@'localhost';



DROP USER IF EXISTS 'Default'@'localhost';
CREATE USER IF NOT EXISTS 'Default'@'localhost' IDENTIFIED BY 'Trigg3rs!';
GRANT INSERT, SELECT ON 5to_boleteria.Usuario TO 'Default'@'localhost';
GRANT SELECT, INSERT, UPDATE ON 5to_boleteria.RefreshTokens TO 'Default'@'localhost';

COMMIT;