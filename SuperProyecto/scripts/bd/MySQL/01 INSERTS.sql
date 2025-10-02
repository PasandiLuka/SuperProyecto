INSERT INTO Local (direccion, capacidadMax)
VALUES 
   ('Av. Siempre Viva 123', 1000),
   ('Calle Falsa 456', 500),
   ('Plaza Central 1', 2000),
   ('Av. Libertad 321', 1500),
   ('Calle Nueva 789', 800),
   ('Av. Sol 234', 1200),
   ('Plaza Norte 77', 1800),
   ('Av. Luna 456', 900),
   ('Calle Este 101', 700),
   ('Av. Oeste 202', 1300),
   ('Plaza Sur 303', 600),
   ('Av. Centro 404', 1100),
   ('Calle Vieja 505', 1400),
   ('Av. Mar 606', 1600),
   ('Plaza Oeste 707', 1000);


INSERT INTO Sector (sector)
VALUES
   ('VIP'),
   ('General'),
   ('Preferencial'),
   ('Platea'),
   ('Balcon'),
   ('Campo'),
   ('Tribuna'),
   ('Palco'),
   ('Grada'),
   ('Galeria'),
   ('Platea Baja'),
   ('Platea Alta'),
   ('Zona A'),
   ('Zona B'),
   ('Zona C');


INSERT INTO Tarifa (precio)
VALUES
   (100.00),
   (150.00),
   (200.00),
   (250.00),
   (300.00),
   (350.00),
   (400.00),
   (450.00),
   (500.00),
   (550.00),
   (600.00),
   (650.00),
   (700.00),
   (750.00),
   (800.00);


INSERT INTO Cliente (DNI, nombre, apellido, telefono)
VALUES
   (1, 'Juan', 'Perez', 12345678),
   (2, 'Maria', 'Lopez', 87654321),
   (3, 'Carlos', 'Gomez', 23456789),
   (4, 'Ana', 'Martinez', 98765432),
   (5, 'Luis', 'Rodriguez', 34567890),
   (6, 'Sofia', 'Fernandez', 45678901),
   (7, 'Pedro', 'Sanchez', 56789012),
   (8, 'Laura', 'Diaz', 67890123),
   (9, 'Diego', 'Torres', 78901234),
   (10, 'Marta', 'Ruiz', 89012345),
   (11, 'Jose', 'Vargas', 90123456),
   (12, 'Elena', 'Castro', 11223344),
   (13, 'Andres', 'Gonzalez', 22334455),
   (14, 'Clara', 'Ramos', 33445566),
   (15, 'Ricardo', 'Flores', 44556677);


INSERT INTO Evento (idLocal, fechaIncio, descripcion)
VALUES
   (1, '2025-10-01', 'Concierto Rock'),
   (2, '2025-10-05', 'Teatro Infantil'),
   (3, '2025-10-10', 'Opera'),
   (4, '2025-10-15', 'Festival de Jazz'),
   (5, '2025-10-20', 'Obra de Teatro'),
   (6, '2025-10-25', 'Conferencia'),
   (7, '2025-10-30', 'Concierto Pop'),
   (8, '2025-11-01', 'Exposicion de Arte'),
   (9, '2025-11-05', 'Ballet'),
   (10, '2025-11-10', 'Show de Magia'),
   (11, '2025-11-15', 'Stand up Comedy'),
   (12, '2025-11-20', 'Festival de Cine'),
   (13, '2025-11-25', 'Evento Deportivo'),
   (14, '2025-11-30', 'Taller de Danza'),
   (15, '2025-12-05', 'Festival Gastronómico');


INSERT INTO Funcion (idEvento, descripcion, fechaHora)
VALUES
   (1, 'Primera funcion', '2025-10-01 20:00:00'),
   (2, 'Funcion matutina', '2025-10-05 10:00:00'),
   (3, 'Funcion nocturna', '2025-10-10 21:00:00'),
   (4, 'Segunda funcion', '2025-10-15 18:00:00'),
   (5, 'Funcion especial', '2025-10-20 19:30:00'),
   (6, 'Conferencia principal', '2025-10-25 14:00:00'),
   (7, 'Concierto segunda parte', '2025-10-30 22:00:00'),
   (8, 'Exposicion apertura', '2025-11-01 09:00:00'),
   (9, 'Ballet gala', '2025-11-05 20:30:00'),
   (10, 'Show de magia', '2025-11-10 17:00:00'),
   (11, 'Comedia stand up', '2025-11-15 21:00:00'),
   (12, 'Festival cine', '2025-11-20 16:00:00'),
   (13, 'Partido final', '2025-11-25 19:00:00'),
   (14, 'Taller danza inicio', '2025-11-30 15:00:00'),
   (15, 'Festival gastronomico inicio', '2025-12-05 12:00:00');


INSERT INTO Orden (numeroOrden, fechaCompra, precioTotal, DNI)
VALUES
   (1, '2025-09-01 12:00:00', 300.00, 1),
   (2, '2025-09-02 13:00:00', 450.00, 2),
   (3, '2025-09-03 14:00:00', 200.00, 3),
   (4, '2025-09-04 15:00:00', 250.00, 4),
   (5, '2025-09-05 16:00:00', 500.00, 5),
   (6, '2025-09-06 17:00:00', 350.00, 6),
   (7, '2025-09-07 18:00:00', 400.00, 7),
   (8, '2025-09-08 19:00:00', 600.00, 8),
   (9, '2025-09-09 20:00:00', 700.00, 9),
   (10, '2025-09-10 21:00:00', 800.00, 10),
   (11, '2025-09-11 22:00:00', 650.00, 11),
   (12, '2025-09-12 23:00:00', 550.00, 12),
   (13, '2025-09-13 10:00:00', 150.00, 13),
   (14, '2025-09-14 11:00:00', 100.00, 14),
   (15, '2025-09-15 12:00:00', 450.00, 15);


INSERT INTO Entrada (idEntrada, idTarifa, numeroOrden, idFuncion, QR, usada)
VALUES
   (1, 1, 1, 1, 'QR001', FALSE),
   (2, 2, 2, 2, 'QR002', FALSE),
   (3, 3, 3, 3, 'QR003', FALSE),
   (4, 4, 4, 4, 'QR004', FALSE),
   (5, 5, 5, 5, 'QR005', FALSE),
   (6, 6, 6, 6, 'QR006', FALSE),
   (7, 7, 7, 7, 'QR007', FALSE),
   (8, 8, 8, 8, 'QR008', FALSE),
   (9, 9, 9, 9, 'QR009', FALSE),
   (10, 10, 10, 10, 'QR010', FALSE),
   (11, 11, 11, 11, 'QR011', FALSE),
   (12, 12, 12, 12, 'QR012', FALSE),
   (13, 13, 13, 13, 'QR013', FALSE),
   (14, 14, 14, 14, 'QR014', FALSE),
   (15, 15, 15, 15, 'QR015', FALSE);