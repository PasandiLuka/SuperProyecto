DROP DATABASE IF EXISTS bd_boleteria;
CREATE DATABASE bd_boleteria;
USE bd_boleteria;


CREATE TABLE Local(
   idLocal INT AUTO_INCREMENT,
   direccion VARCHAR(45),
   capacidadMax INT,

   CONSTRAINT PK_Local PRIMARY KEY (idLocal)
);


CREATE TABLE Sector(
   idSector INT AUTO_INCREMENT,
   sector VARCHAR(45),

   CONSTRAINT PK_Sector PRIMARY KEY (idSector)
);


CREATE TABLE Tarifa(
   idTarifa INT AUTO_INCREMENT,
   precio DECIMAL(10,2),

   CONSTRAINT PK_Tarifa PRIMARY KEY (idTarifa)
);


CREATE TABLE Cliente(
   DNI INT AUTO_INCREMENT,
   nombre VARCHAR(45),
   apellido VARCHAR(45),
   telefono int,

   CONSTRAINT pk_Cliente PRIMARY KEY (DNI)
);


CREATE TABLE Evento(
   idEvento INT AUTO_INCREMENT,
   idLocal INT,
   fechaIncio DATE,
   descripcion VARCHAR(45),

   CONSTRAINT PK_Evento PRIMARY KEY (idEvento),
   CONSTRAINT FK_Evento_Local FOREIGN KEY (idLocal)
       REFERENCES Local (idLocal)
);


CREATE TABLE Funcion(
   idFuncion INT AUTO_INCREMENT,
   idEvento INT,
   descripcion VARCHAR(45),
   fechaHora DATETIME,

   CONSTRAINT PK_Funcion PRIMARY KEY (idFuncion),
   CONSTRAINT FK_Funcion_Evento FOREIGN KEY (idEvento)
       REFERENCES Evento (idEvento)
);


CREATE TABLE Orden(
   numeroOrden INT,
   fechaHoraCompra DATETIME,
   precioTotal DECIMAL,
   DNI INT,

   CONSTRAINT PK_Orden PRIMARY KEY (numeroOrden),
   CONSTRAINT FK_Orden_Cliente FOREIGN KEY (DNI)
       REFERENCES Cliente (DNI)
);


CREATE TABLE Entrada(
   idEntrada INT,
   idTarifa INT,
   numeroOrden INT,
   idFuncion INT,
   QR VARCHAR(45),
   usada bool,

   CONSTRAINT PK_Entrada PRIMARY KEY (idEntrada),
   CONSTRAINT FK_Entrada_Tarifa FOREIGN KEY (idTarifa)
       REFERENCES Tarifa (idTarifa),
   CONSTRAINT FK_Entrada_Orden FOREIGN KEY (numeroOrden)
       REFERENCES Orden (numeroOrden),
   CONSTRAINT FK_Entrada_Funcion FOREIGN KEY (idFuncion)
       REFERENCES Funcion (idFuncion)
);