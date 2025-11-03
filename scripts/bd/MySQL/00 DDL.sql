SET AUTOCOMMIT=0;
START TRANSACTION;

-- Tabla Usuario
CREATE TABLE Usuario (
    idUsuario INT PRIMARY KEY AUTO_INCREMENT,
    email VARCHAR(45) NOT NULL UNIQUE,
    passwordHash VARCHAR(255) NOT NULL,
    rol INT NOT NULL
);

-- Tabla Local
CREATE TABLE Local (
    idLocal INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(100) NOT NULL,
    direccion VARCHAR(200) NOT NULL,
    eliminado BOOL DEFAULT FALSE
);

-- Tabla Evento
CREATE TABLE Evento (
    idEvento INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(100) NOT NULL,
    descripcion VARCHAR(300),
    publicado BOOLEAN DEFAULT FALSE,
    cancelado BOOLEAN DEFAULT FALSE
);

-- Tabla Funcion
CREATE TABLE Funcion (
    idFuncion INT PRIMARY KEY AUTO_INCREMENT,
    idEvento INT NOT NULL,
    fechaHora DATETIME NOT NULL,
    cancelada BOOLEAN DEFAULT FALSE,
    FOREIGN KEY (idEvento) REFERENCES Evento(idEvento)
);

-- Tabla Sector
CREATE TABLE Sector (
    idSector INT PRIMARY KEY AUTO_INCREMENT,
    idLocal INT NOT NULL,
    nombre VARCHAR(100) NOT NULL,
    eliminado BOOL DEFAULT FALSE,
    FOREIGN KEY (idLocal) REFERENCES Local(idLocal)
);

-- Tabla Tarifa
CREATE TABLE Tarifa (
    idTarifa INT PRIMARY KEY AUTO_INCREMENT,
    idFuncion INT NOT NULL,
    idSector INT NOT NULL,
    precio DECIMAL(10,2) NOT NULL,
    stock INT NOT NULL,
    activo BOOLEAN DEFAULT TRUE,
    FOREIGN KEY (idFuncion) REFERENCES Funcion(idFuncion),
    FOREIGN KEY (idSector) REFERENCES Sector(idSector)
);

-- Tabla Cliente
CREATE TABLE Cliente (
    idCliente INT PRIMARY KEY AUTO_INCREMENT,
    idUsuario INT NOT NULL,
    DNI INT NOT NULL,
    nombre VARCHAR(45) NOT NULL,
    apellido VARCHAR(45) NOT NULL,

    CONSTRAINT FK_Cliente_Usuario FOREIGN KEY (idUsuario)
        REFERENCES Usuario (idUsuario)
);

-- Tabla Orden
CREATE TABLE Orden (
    idOrden INT PRIMARY KEY AUTO_INCREMENT,
    idCliente INT NOT NULL,
    fecha DATETIME NOT NULL,
    pagada BOOL NOT NULL DEFAULT FALSE,
    cancelada BOOL NOT NULL DEFAULT FALSE,
    total DECIMAL(10,2) NOT NULL DEFAULT 0,
    FOREIGN KEY (idCliente) REFERENCES Cliente(idCliente)
);

-- Tabla Entrada
CREATE TABLE Entrada (
    idEntrada INT PRIMARY KEY AUTO_INCREMENT,
    idOrden INT NOT NULL,
    idTarifa INT NOT NULL,
    anulada BOOLEAN DEFAULT FALSE,
    usada BOOLEAN DEFAULT FALSE,
    FOREIGN KEY (idOrden) REFERENCES Orden(idOrden),
    FOREIGN KEY (idTarifa) REFERENCES Tarifa(idTarifa)
);

CREATE TABLE Qr (
    idQr INT PRIMARY KEY AUTO_INCREMENT,
    idEntrada INT NOT NULL UNIQUE,
    url VARCHAR(255) NOT NULL,
    CONSTRAINT FK_Qr_Entrada FOREIGN KEY (idEntrada) 
        REFERENCES Entrada(idEntrada)
);

CREATE TABLE RefreshTokens (
    idRefreshToken INT AUTO_INCREMENT PRIMARY KEY,
    idUsuario INT NOT NULL,
    refreshToken VARCHAR(200) NOT NULL,
    emitido DATETIME NOT NULL,
    expiracion DATETIME NOT NULL,
    revocado BOOLEAN DEFAULT FALSE,
    CONSTRAINT FK_RefreshTokens_Usuario FOREIGN KEY (idUsuario) 
        REFERENCES Usuario(idUsuario)
);

COMMIT;