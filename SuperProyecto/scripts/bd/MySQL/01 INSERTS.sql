-- Local
INSERT INTO Local VALUES
(1,'Teatro Central','Calle A 123'),
(2,'Auditorio Norte','Calle B 456'),
(3,'Sala Este','Calle C 789'),
(4,'Centro Cultural','Av D 101'),
(5,'Teatro Sur','Av E 202'),
(6,'Sala Oeste','Calle F 303'),
(7,'Teatro Principal','Av G 404'),
(8,'Auditorio Principal','Calle H 505'),
(9,'Sala Multiuso','Av I 606'),
(10,'Centro de Convenciones','Av J 707');

-- Sector
INSERT INTO Sector VALUES
(1,1,'Platea',200),
(2,1,'Balcon',150),
(3,2,'General',300),
(4,2,'VIP',50),
(5,3,'Principal',100),
(6,4,'Lateral',80),
(7,5,'Superior',120),
(8,6,'Inferior',90),
(9,7,'Central',250),
(10,8,'Galeria',60);

-- Evento
INSERT INTO Evento VALUES
(1,'Concierto Rock','Rock Nacional', '2025-10-01 10:00:00', true),
(2,'Obra Teatro','Drama Historico', '2025-10-02 11:00:00', true),
(3,'Musical Infantil','Musical para niños', '2025-10-03 12:00:00', false),
(4,'Conferencia Tech','Innovación y Tecnología', '2025-10-04 13:00:00', true),
(5,'Exposicion Arte','Arte Moderno', '2025-10-05 14:00:00', false),
(6,'Show Magia','Espectaculo de magia', '2025-10-06 15:00:00', true),
(7,'Concierto Jazz','Jazz Internacional', '2025-10-07 16:00:00', false),
(8,'Feria Gastronomica','Comida del Mundo', '2025-10-08 17:00:00', true),
(9,'Taller Fotografia','Fotografía Digital', '2025-10-09 18:00:00', false),
(10,'Festival Cine','Cine Independiente', '2025-10-10 19:00:00', true);

-- Funcion
INSERT INTO Funcion VALUES
(1,1,1,'2025-10-10 20:00:00', false),
(2,1,2,'2025-10-10 21:00:00', false),
(3,2,3,'2025-10-11 19:00:00', false),
(4,2,4,'2025-10-11 20:00:00', false),
(5,3,5,'2025-10-12 15:00:00', false),
(6,4,6,'2025-10-13 10:00:00', false),
(7,5,7,'2025-10-14 18:00:00', false),
(8,6,8,'2025-10-15 17:00:00', false),
(9,7,9,'2025-10-16 20:00:00', false),
(10,8,10,'2025-10-17 19:00:00', false);

-- Tarifa
INSERT INTO Tarifa VALUES
(1,1,'General',150.00,100),
(2,1,'VIP',300.00,50),
(3,2,'General',120.00,100),
(4,3,'General',80.00,150),
(5,4,'VIP',200.00,40),
(6,5,'General',50.00,200),
(7,6,'VIP',250.00,30),
(8,7,'General',100.00,120),
(9,8,'VIP',180.00,60),
(10,9,'General',90.00,80);

-- Cliente
INSERT INTO Cliente VALUES
(1,'Juan','Perez','juan.perez@email.com', 10),
(2,'Maria','Gomez','maria.gomez@email.com', 12),
(3,'Pedro','Lopez','pedro.lopez@email.com', 13),
(4,'Ana','Martinez','ana.martinez@email.com', 14),
(5,'Luis','Rodriguez','luis.rodriguez@email.com', 15),
(6,'Carla','Fernandez','carla.fernandez@email.com', 16),
(7,'Diego','Sanchez','diego.sanchez@email.com', 17),
(8,'Lucia','Torres','lucia.torres@email.com', 18),
(9,'Martin','Diaz','martin.diaz@email.com', 19),
(10,'Sofia','Vega','sofia.vega@email.com', 20);

-- Orden
INSERT INTO Orden VALUES
(1,1,'2025-10-01 09:00:00',150.00),
(2,2,'2025-10-02 10:00:00',300.00),
(3,3,'2025-10-03 11:00:00',120.00),
(4,4,'2025-10-04 12:00:00',80.00),
(5,5,'2025-10-05 13:00:00',50.00),
(6,6,'2025-10-06 14:00:00',250.00),
(7,7,'2025-10-07 15:00:00',100.00),
(8,8,'2025-10-08 16:00:00',180.00),
(9,9,'2025-10-09 17:00:00',90.00),
(10,10,'2025-10-10 18:00:00',120.00);

-- Entrada
INSERT INTO Entrada VALUES
(1,1,1,'QR001',false),
(2,2,1,'QR002',true),
(3,3,2,'QR003',false),
(4,4,3,'QR004',true),
(5,5,4,'QR005',false),
(6,6,5,'QR006',false),
(7,7,6,'QR007',true),
(8,8,7,'QR008',false),
(9,9,8,'QR009',true),
(10,10,9,'QR010',false);

-- Usuario
INSERT INTO Usuario VALUES
(1,"Administrador",'juanp','hash123'),
(2,"Cliente",'mariag','hash234'),
(3,"Cliente",'pedrol','hash345'),
(4,"Cliente",'anam','hash456'),
(5,"Cliente",'luisr','hash567'),
(6,"Cliente",'carlaf','hash678'),
(7,"Cliente",'diegos','hash789'),
(8,"Cliente",'luciator','hash890'),
(9,"Cliente",'martind','hash901'),
(10,"Cliente",'sofiav','hash012');