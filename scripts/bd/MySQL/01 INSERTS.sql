-- ==========================
-- 1. LOCAL
-- ==========================
INSERT INTO Local (nombre, direccion) VALUES
('Teatro Colón', 'Cerrito 628'),
('Luna Park', 'Bouchard 465'),
('Gran Rex', 'Av. Corrientes 857');

-- ==========================
-- 2. EVENTO
-- ==========================
INSERT INTO Evento (nombre, descripcion, fechaPublicacion, publicado) VALUES
('Concierto Sinfónico', 'Presentación de la Orquesta Nacional', '2025-09-01 18:00:00', TRUE),
('Obra de Teatro', 'Comedia musical en vivo', '2025-08-15 10:00:00', TRUE),
('Festival de Jazz', 'Edición anual con artistas internacionales', '2025-07-20 09:00:00', FALSE);

-- ==========================
-- 3. USUARIO
-- ==========================
INSERT INTO Usuario (email, passwordHash, rol) VALUES
('juanperez@gmail.com', sha2("123456", 256), 'Cliente'),
('maria.gomez@gmail.com', sha2("123456", 256), 'Cliente'),
('admin@gmail.com', sha2("123456", 256), 'Administrador');

 -- ==========================
-- 4. SECTOR
-- ==========================
INSERT INTO Sector (idLocal, nombre, capacidad) VALUES
(1, 'Platea Baja', 300),
(1, 'Platea Alta', 500),
(2, 'VIP', 100);

-- ==========================
-- 5. CLIENTE
-- ==========================
INSERT INTO Cliente (DNI, idUsuario, nombre, apellido, telefono) VALUES
(12345678, 1, 'Juan', 'Pérez', 1123456789),
(23456789, 2, 'María', 'Gómez', 1198765432),
(34567890, 3, 'Carlos', 'López', 1134567890);

-- ==========================
-- 7. TARIFA
-- ==========================
INSERT INTO Tarifa (idSector, precio) VALUES
(1, 2500.00),
(2, 1800.00),
(3, 4000.00);

-- ==========================
-- 8. FUNCION
-- ==========================
INSERT INTO Funcion (idTarifa, fechaHora, stock, cancelada) VALUES
(1, '2025-10-10 20:00:00', 100, FALSE),
(2, '2025-10-12 21:30:00', 0, FALSE),
(3, '2025-10-15 19:00:00', 100, TRUE);