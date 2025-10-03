INSERT INTO Local (direccion, capacidadMax) 
VALUES ('Av. Siempre Viva 123', 100);

INSERT INTO Sector (sector) 
VALUES ('Platea');

INSERT INTO Tarifa (precio) 
VALUES (1500.00);

INSERT INTO Cliente (DNI, nombre, apellido, telefono) 
VALUES (12345678, 'Juan', 'Pérez', 1122334455);

INSERT INTO Evento (idLocal, fechaIncio, descripcion) 
VALUES (1, '2025-10-01', 'Concierto de prueba');

INSERT INTO Funcion (idEvento, descripcion, fechaHora) 
VALUES (1, 'Función Principal', '2025-10-02 20:00:00');

INSERT INTO Orden (numeroOrden, DNI, fechaCompra, precioTotal) 
VALUES (1, 12345678, '2025-09-30 15:00:00', 3000.00);

INSERT INTO Entrada (idEntrada, idTarifa, numeroOrden, idFuncion, QR, usada) 
VALUES (1, 1, 1, 1, 'QR12345', false);