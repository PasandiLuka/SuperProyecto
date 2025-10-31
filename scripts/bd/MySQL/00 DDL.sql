SET AUTOCOMMIT=0;
START TRANSACTION;

CREATE TABLE Local (
    idLocal INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(45) NOT NULL,
    direccion VARCHAR(200) NOT NULL
);

CREATE TABLE Evento (
    idEvento INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(45) NOT NULL,
    descripcion VARCHAR(200),
    fechaPublicacion DATETIME NOT NULL,
    publicado BOOLEAN DEFAULT FALSE,
    cancelado BOOLEAN DEFAULT FALSE
);

CREATE TABLE Funcion (
    idFuncion INT PRIMARY KEY AUTO_INCREMENT,
    idEvento INT NOT NULL,
    fechaHora DATETIME NOT NULL,
    stock INT NOT NULL,
    cancelada BOOLEAN DEFAULT FALSE,
    CONSTRAINT FK_Funcion_Evento FOREIGN KEY (idEvento) 
        REFERENCES Evento(idEvento)
);

CREATE TABLE Tarifa (
    idTarifa INT PRIMARY KEY AUTO_INCREMENT,
    precio DECIMAL(10,2) NOT NULL
);

CREATE TABLE Sector (
    idSector INT PRIMARY KEY AUTO_INCREMENT,
    idLocal INT NOT NULL,
    idFuncion INT NOT NULL,
    idTarifa INT NOT NULL,
    nombre VARCHAR(45) NOT NULL,
    capacidad INT NOT NULL,
    CONSTRAINT FK_Sector_Local FOREIGN KEY (idLocal) 
        REFERENCES Local(idLocal),
    CONSTRAINT FK_Sector_Funcion FOREIGN KEY (idFuncion) 
        REFERENCES Funcion(idFuncion),
    CONSTRAINT FK_Sector_Tarifa FOREIGN KEY (idTarifa) 
        REFERENCES Tarifa(idTarifa)
);

CREATE TABLE Usuario (
    idUsuario INT PRIMARY KEY AUTO_INCREMENT,
    email VARCHAR(45) NOT NULL UNIQUE,
    passwordHash VARCHAR(255) NOT NULL,
    rol INT NOT NULL
);

CREATE TABLE Cliente (
    DNI INT PRIMARY KEY,
    idUsuario INT UNIQUE NOT NULL,
    nombre VARCHAR(45) NOT NULL,
    apellido VARCHAR(45) NOT NULL,
    telefono INT,
    CONSTRAINT FK_Cliente_Usuario FOREIGN KEY (idUsuario)
        REFERENCES Usuario(idUsuario)
);

CREATE TABLE RefreshTokens (
    idRefreshToken INT AUTO_INCREMENT PRIMARY KEY,
    idUsuario INT NOT NULL,
    refreshToken VARCHAR(200) NOT NULL,
    expiracion DATETIME NOT NULL,
    revocado BOOLEAN DEFAULT FALSE,
    CONSTRAINT FK_RefreshTokens_Usuario FOREIGN KEY (idUsuario) 
        REFERENCES Usuario(idUsuario)
);

CREATE TABLE Orden (
    idOrden INT PRIMARY KEY AUTO_INCREMENT,
    DNI INT NOT NULL,
    idSector INT NOT NULL,
    fecha DATETIME NOT NULL,
    pagada BOOLEAN DEFAULT FALSE,
    CONSTRAINT FK_Orden_Cliente FOREIGN KEY (DNI) 
        REFERENCES Cliente(DNI),
    CONSTRAINT FK_Orden_Sector FOREIGN KEY (idSector) 
        REFERENCES Sector(idSector)
);

CREATE TABLE Entrada (
    idEntrada INT PRIMARY KEY AUTO_INCREMENT,
    idOrden INT NOT NULL,
    usada BOOLEAN DEFAULT FALSE,
    CONSTRAINT FK_Entrada_Orden FOREIGN KEY (idOrden) 
        REFERENCES Orden(idOrden)
);

CREATE TABLE Qr (
    idQr INT PRIMARY KEY AUTO_INCREMENT,
    idEntrada INT NOT NULL UNIQUE,
    url VARCHAR(255) NOT NULL,
    CONSTRAINT FK_Qr_Entrada FOREIGN KEY (idEntrada) 
        REFERENCES Entrada(idEntrada)
);

COMMIT;