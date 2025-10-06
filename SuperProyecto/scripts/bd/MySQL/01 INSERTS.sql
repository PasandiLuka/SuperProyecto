-- LOCAL
INSERT INTO Local (nombre, direccion) 
    VALUES ('Teatro Colón', 'Cerrito 628'),
           ('Luna Park', 'Bouchard 465'),
           ('Gran Rex', 'Av. Corrientes 857');

-- EVENTO
INSERT INTO Evento (nombre, descripcion, fechaPublicacion, publicado) 
    VALUES ('Concierto A', 'Música clásica', '2025-10-06 21:00:00', TRUE),
           ('Stand Up B', 'Show de comedia', '2025-10-07 21:00:00', TRUE),
           ('Obra C', 'Teatro contemporáneo', '2025-10-08 21:00:00', FALSE);



-- USUARIO
INSERT INTO Usuario (email, passwordHash, rol) 
    VALUES ('admin@gmail.com', sha2("1234", 256), "Administrador"),
           ('organizador@gmail.com', sha2("1234", 256), "Organizador"),
           ('cliente@gmail.com', sha2("1234", 256), "Cliente"),
           ('cliente2@gmail.com', sha2("1234", 256), "Cliente");

-- SECTOR
INSERT INTO Sector (idLocal, nombre, capacidad) 
    VALUES (1, 'Platea Baja', 500),
           (2, 'Campo', 1000),
           (3, 'Palco', 300);

-- CLIENTE
INSERT INTO Cliente (DNI, idUsuario, nombre, apellido, telefono) 
    VALUES (12345678, 2, 'Juan', 'Pérez', 1123456789),
           (23456789, 3, 'María', 'Gómez', 1198765432),
           (34567890, 4, 'Carlos', 'López', 1111122233);

-- ORDEN
INSERT INTO Orden (DNI, fecha, total) 
    VALUES (12345678, '2025-10-06 21:00:00', 2500.50),
           (23456789, '2025-10-07 21:00:00', 3100.00),
           (34567890, '2025-10-08 21:00:00', 1800.75);

-- FUNCION
INSERT INTO Funcion (idEvento, idSector, fechaHora, cancelada) 
    VALUES (1, 1, '2025-10-06 21:00:00', FALSE),
           (2, 2, '2025-10-07 21:00:00', FALSE),
           (3, 3, '2025-10-08 21:00:00', TRUE);

-- TARIFA
INSERT INTO Tarifa (idFuncion, nombre, precio, stock) 
    VALUES (1, 'General', 1500.00, 100),
           (2, 'VIP', 3000.00, 50),
           (3, 'Promocional', 1000.00, 30);
