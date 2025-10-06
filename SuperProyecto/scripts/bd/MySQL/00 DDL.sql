CREATE TABLE Local (
    idLocal INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(45),
    direccion VARCHAR(45)
);

CREATE TABLE Evento (
    idEvento INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(45),
    descripcion VARCHAR(45),
    fechaPublicacion DATETIME,
    publicado BOOLEAN
);

CREATE TABLE Usuario (
    idUsuario INT AUTO_INCREMENT PRIMARY KEY,
    email VARCHAR(45),
    passwordHash VARCHAR(255),
    rol VARCHAR(45)
);

CREATE TABLE Sector (
    idSector INT AUTO_INCREMENT PRIMARY KEY,
    idLocal INT,
    nombre VARCHAR(45),
    capacidad INT,

    CONSTRAINT FK_Sector_Local FOREIGN KEY (idLocal)
        REFERENCES Local (idLocal)
);

CREATE TABLE Cliente (
    DNI INT PRIMARY KEY,
    idUsuario INT,
    nombre VARCHAR(45),
    apellido VARCHAR(45),
    telefono INT,

    CONSTRAINT FK_Cliente_Usuario FOREIGN KEY (idUsuario)
        REFERENCES Usuario (idUsuario)
);

CREATE TABLE Orden (
    idOrden INT AUTO_INCREMENT PRIMARY KEY,
    DNI INT,
    fecha DATETIME,
    total DECIMAL(10,2),

    CONSTRAINT FK_Orden_Cliente FOREIGN KEY (DNI) 
        REFERENCES Cliente (DNI)
);

CREATE TABLE RefreshTokens (
    idRefreshToken INT AUTO_INCREMENT PRIMARY KEY,
    idUsuario INT NOT NULL,
    token VARCHAR(200) NOT NULL,
    expiracion DATETIME NOT NULL,
    revocado BIT DEFAULT 0,
    FOREIGN KEY (idUsuario) REFERENCES Usuario(idUsuario)
);

CREATE TABLE Funcion (
    idFuncion INT AUTO_INCREMENT PRIMARY KEY,
    idEvento INT,
    idSector INT,
    fechaHora DATETIME,
    cancelada BOOLEAN DEFAULT FALSE,

    CONSTRAINT FK_Funcion_Evento FOREIGN KEY (idEvento) 
        REFERENCES Evento (idEvento),
    CONSTRAINT FK_Funcion_Sector FOREIGN KEY (idSector) 
        REFERENCES Sector (idSector)
);

CREATE TABLE Tarifa (
    idTarifa INT AUTO_INCREMENT PRIMARY KEY,
    idFuncion INT,
    nombre VARCHAR(45),
    precio DECIMAL(10,2),
    stock INT,

    CONSTRAINT FK_Tarifa_Funcion FOREIGN KEY (idFuncion) 
        REFERENCES Funcion (idFuncion)
);

CREATE TABLE Qr (
    idQr INT AUTO_INCREMENT PRIMARY KEY,
    idEntrada INT,
    url VARCHAR (100)
);

CREATE TABLE Entrada (
    idEntrada INT AUTO_INCREMENT PRIMARY KEY,
    idOrden INT,
    idFuncion INT,
    usada BOOLEAN DEFAULT FALSE,

    CONSTRAINT FK_Entrada_Orden FOREIGN KEY (idOrden) 
        REFERENCES Orden (idOrden),
    CONSTRAINT FK_Entrada_Funcion FOREIGN KEY (idFuncion) 
        REFERENCES Funcion (idFuncion)
);