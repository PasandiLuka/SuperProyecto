SET AUTOCOMMIT=0;
START TRANSACTION;

-- Local
INSERT INTO Local (nombre, direccion) VALUES
('Teatro Central', 'Av. Corrientes 1234'),
('Auditorio Norte', 'Calle Mitre 800'),
('Sala Sur', 'Boulevard Belgrano 550');

-- Evento
INSERT INTO Evento (nombre, descripcion, fechaPublicacion, publicado, cancelado) VALUES
('Concierto Rock', 'Banda local en vivo', NOW(), TRUE, FALSE),
('Obra Teatro', 'Comedia romántica', NOW(), TRUE, FALSE),
('Festival Jazz', 'Artistas internacionales', NOW(), FALSE, FALSE);

-- Funcion
INSERT INTO Funcion (idEvento, fechaHora, stock, cancelada) VALUES
(1, '2025-11-10 20:00:00', 200, FALSE),
(2, '2025-11-12 19:00:00', 150, FALSE),
(3, '2025-11-15 21:30:00', 300, FALSE);

-- Tarifa
INSERT INTO Tarifa (precio) VALUES
(5000.00),
(7500.00),
(10000.00);

-- Sector
INSERT INTO Sector (idLocal, idFuncion, idTarifa, nombre, capacidad) VALUES
(1, 1, 1, 'Platea', 150),
(2, 2, 2, 'VIP', 80),
(3, 3, 3, 'General', 200);

-- Usuario
INSERT INTO Usuario (email, passwordHash, rol) VALUES
('admin@test.com', SHA2(123456, 256), 1),
('organizador@test.com', SHA2(123456, 256), 2),
('cliente1@test.com', SHA2(123456, 256), 3),
('cliente2@test.com', SHA2(123456, 256), 3);

-- Cliente
INSERT INTO Cliente (DNI, idUsuario, nombre, apellido, telefono) VALUES
(40234567, 3, 'Ana', 'Perez', 1198765432),
(40345678, 4, 'Carlos', 'Lopez', 1133445566);

COMMIT;