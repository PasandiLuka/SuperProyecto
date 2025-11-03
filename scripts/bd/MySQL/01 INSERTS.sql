SET AUTOCOMMIT=0;
START TRANSACTION;

INSERT INTO Usuario (email, passwordHash, rol) VALUES
('admin@test.com', SHA2(123456, 256), 1),
('organizador@test.com', SHA2(123456, 256), 2),
('cliente1@test.com', SHA2(123456, 256), 3),
('cliente2@test.com', SHA2(123456, 256), 3),
('cliente3@test.com', SHA2(123456, 256), 3),
('cliente4@test.com', SHA2(123456, 256), 3);

-- Cliente
INSERT INTO Cliente (idUsuario, DNI, nombre, apellido) VALUES
(3, 12345678, 'Juan', 'Pérez'),
(4, 23456789, 'Ana', 'Gómez'),
(5, 34567890, 'Luis', 'Martínez'),
(6, 45678901, 'Carla', 'Rivas');

-- Local
INSERT INTO Local (nombre, direccion, eliminado) VALUES
('Teatro Central', 'Av. Siempre Viva 123', FALSE),
('Auditorio Norte', 'Calle Falsa 456', FALSE),
('Centro Cultural Sur', 'Av. Libertad 789', TRUE),   -- Eliminado
('Anfiteatro Parque', 'Ruta 12 KM 5', FALSE);

-- Evento
INSERT INTO Evento (nombre, descripcion, publicado, cancelado) VALUES
('Show de Rock', 'Concierto en vivo', TRUE, FALSE),
('Obra Teatral', 'Comedia romántica', TRUE, FALSE),
('Recital Sinfónico', 'Orquesta completa', TRUE, TRUE), -- Cancelado
('Festival Indie', 'Bandas emergentes', TRUE, FALSE);

-- Funcion
INSERT INTO Funcion (idEvento, idLocal, fechaHora, cancelada) VALUES
(1, 1, '2025-12-20 20:00:00', FALSE),
(2, 2, '2025-12-22 19:00:00', FALSE),
(3, 1, '2025-12-25 18:00:00', TRUE),  -- Función cancelada
(4, 4, '2025-12-30 21:00:00', FALSE);

-- Sector
INSERT INTO Sector (idLocal, nombre, eliminado) VALUES
(1, 'Platea', FALSE),
(1, 'VIP', FALSE),
(2, 'General', FALSE),
(2, 'Lateral', TRUE); -- Eliminado

-- Tarifa (una inactiva y otra sin stock)
INSERT INTO Tarifa (idFuncion, idSector, precio, stock, activo) VALUES
(1, 1, 5000.00, 100, TRUE),
(1, 2, 8000.00, 0, TRUE),   -- Stock agotado
(2, 3, 3000.00, 200, FALSE), -- Tarifa inactiva
(4, 1, 6000.00, 40, TRUE);

-- Orden
INSERT INTO Orden (idCliente, idSector, fecha, pagada, cancelada, total) VALUES
(1, 1, '2025-12-01 10:00:00', FALSE, FALSE, 5000.00),
(2, 2, '2025-12-02 11:30:00', FALSE, TRUE, 8000.00),
(3, 3, '2025-12-03 12:45:00', FALSE, FALSE, 3000.00),
(4, 1, '2025-12-04 09:15:00', FALSE, FALSE, 6000.00);

-- Entrada (casos variados)
INSERT INTO Entrada (idOrden, idTarifa, anulada, usada) VALUES
(1, 1, FALSE, FALSE),
(2, 2, TRUE, FALSE),
(3, 2, FALSE, FALSE),
(4, 4, FALSE, FALSE);


COMMIT;
