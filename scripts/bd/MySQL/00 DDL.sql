

CREATE TABLE Local (
    idLocal INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(100) NOT NULL,
    direccion VARCHAR(255) NOT NULL
);

CREATE TABLE Evento (
    idEvento INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(100) NOT NULL,
    descripcion TEXT,
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
    FOREIGN KEY (idEvento) REFERENCES Evento(idEvento)
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
    nombre VARCHAR(100) NOT NULL,
    capacidad INT NOT NULL,
    FOREIGN KEY (idLocal) REFERENCES Local(idLocal),
    FOREIGN KEY (idFuncion) REFERENCES Funcion(idFuncion),
    FOREIGN KEY (idTarifa) REFERENCES Tarifa(idTarifa)
);

CREATE TABLE Usuario (
    idUsuario INT PRIMARY KEY AUTO_INCREMENT,
    email VARCHAR(150) NOT NULL UNIQUE,
    passwordHash VARCHAR(255) NOT NULL,
    rol VARCHAR(50) NOT NULL
);

CREATE TABLE Cliente (
    DNI INT PRIMARY KEY,
    idUsuario INT UNIQUE NOT NULL,
    nombre VARCHAR(100) NOT NULL,
    apellido VARCHAR(100) NOT NULL,
    telefono VARCHAR(20),
    FOREIGN KEY (idUsuario) REFERENCES Usuario(idUsuario)
);

CREATE TABLE RefreshTokens (
    idRefreshToken INT PRIMARY KEY AUTO_INCREMENT,
    idUsuario INT NOT NULL,
    token VARCHAR(500) NOT NULL,
    expiracion DATETIME NOT NULL,
    revocado BOOLEAN DEFAULT FALSE,
    FOREIGN KEY (idUsuario) REFERENCES Usuario(idUsuario)
);

CREATE TABLE Orden (
    idOrden INT PRIMARY KEY AUTO_INCREMENT,
    DNI INT NOT NULL,
    idSector INT NOT NULL,
    fecha DATETIME NOT NULL,
    pagada BOOLEAN DEFAULT FALSE,
    FOREIGN KEY (DNI) REFERENCES Cliente(DNI),
    FOREIGN KEY (idSector) REFERENCES Sector(idSector)
);

CREATE TABLE Entrada (
    idEntrada INT PRIMARY KEY AUTO_INCREMENT,
    idOrden INT NOT NULL,
    usada BOOLEAN DEFAULT FALSE,
    FOREIGN KEY (idOrden) REFERENCES Orden(idOrden)
);

CREATE TABLE Qr (
    idQr INT PRIMARY KEY AUTO_INCREMENT,
    idEntrada INT NOT NULL UNIQUE,
    url VARCHAR(255) NOT NULL,
    FOREIGN KEY (idEntrada) REFERENCES Entrada(idEntrada)
);