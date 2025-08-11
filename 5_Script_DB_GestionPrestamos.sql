USE master
GO
-- -----------------------------------------------------
-- Creación de la base de datos (si no existe)
-- -----------------------------------------------------
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'GestionPrestamos')
BEGIN
    CREATE DATABASE GestionPrestamos;
END;
GO

USE GestionPrestamos;
GO

-- -----------------------------------------------------
-- Tabla `Monedas`
-- Almacena los tipos de moneda soportados.
-- -----------------------------------------------------
CREATE TABLE Monedas (
    IdMoneda INT IDENTITY(1,1) NOT NULL,
    Codigo VARCHAR(3) NOT NULL UNIQUE,
    Nombre VARCHAR(50) NOT NULL,
    Simbolo VARCHAR(5) NOT NULL,
    PRIMARY KEY (IdMoneda)
);
GO

-- -----------------------------------------------------
-- Tabla `Clientes`
-- Almacena la información de los clientes.
-- -----------------------------------------------------
CREATE TABLE Clientes (
    IdCliente INT IDENTITY(1,1) NOT NULL,
    Nombre VARCHAR(100) NOT NULL,
    Apellido VARCHAR(100) NOT NULL,
    NumeroDocumento VARCHAR(50) NOT NULL UNIQUE,
    Telefono VARCHAR(20) NULL,
    Email VARCHAR(100) NULL UNIQUE,
    PRIMARY KEY (IdCliente)
);
GO

-- -----------------------------------------------------
-- Tabla `Usuarios`
-- Almacena las credenciales de los clientes para acceder al sistema.
-- -----------------------------------------------------
CREATE TABLE Usuarios (
    IdUsuario INT IDENTITY(1,1) NOT NULL,
    Username VARCHAR(100) NOT NULL UNIQUE,
    PasswordHash VARCHAR(255) NOT NULL,
    IdCliente INT NOT NULL UNIQUE,
    PRIMARY KEY (IdUsuario),
    FOREIGN KEY (IdCliente) REFERENCES Clientes(IdCliente)
);
GO

-- -----------------------------------------------------
-- Tabla `TipoCuenta`
-- Almacena los tipos de cuentas bancarias.
-- -----------------------------------------------------
CREATE TABLE TipoCuenta (
    IdTipoCuenta INT IDENTITY(1,1) NOT NULL,
    NombreTipo VARCHAR(50) NOT NULL UNIQUE,
    Descripcion VARCHAR(255) NULL,
    PRIMARY KEY (IdTipoCuenta)
);
GO

-- -----------------------------------------------------
-- Tabla `Bancos`
-- Almacena la información de otros bancos externos.
-- -----------------------------------------------------
CREATE TABLE Bancos (
    IdBanco INT IDENTITY(1,1) NOT NULL,
    NombreBanco VARCHAR(100) NOT NULL UNIQUE,
    CodigoBanco VARCHAR(20) NULL UNIQUE,
    PRIMARY KEY (IdBanco)
);
GO

-- -----------------------------------------------------
-- Tabla `CuentasBancarias`
-- Almacena las cuentas bancarias de los clientes.
-- -----------------------------------------------------
CREATE TABLE CuentasBancarias (
    IdCuenta INT IDENTITY(1,1) NOT NULL,
    IdCliente INT NOT NULL,
    IdTipoCuenta INT NOT NULL,
    IdMoneda INT NOT NULL,
    NumeroCuenta VARCHAR(50) NOT NULL UNIQUE,
    Saldo DECIMAL(18, 4) NOT NULL DEFAULT 0.0000,
    FechaApertura DATETIME NOT NULL DEFAULT GETDATE(),
    PRIMARY KEY (IdCuenta),
    FOREIGN KEY (IdCliente) REFERENCES Clientes(IdCliente),
    FOREIGN KEY (IdTipoCuenta) REFERENCES TipoCuenta(IdTipoCuenta),
    FOREIGN KEY (IdMoneda) REFERENCES Monedas(IdMoneda)
);
GO

-- -----------------------------------------------------
-- Tabla `Transacciones`
-- Almacena cada evento de transacción.
-- -----------------------------------------------------
CREATE TABLE Transacciones (
    IdTransaccion INT IDENTITY(1,1) NOT NULL,
    IdCuentaOrigen INT NULL,
    IdCuentaDestino INT NULL,
    TipoTransaccion VARCHAR(50) NOT NULL,
    Monto DECIMAL(18, 4) NOT NULL,
    IdMoneda INT NOT NULL,
    FechaHoraTransaccion DATETIME NOT NULL DEFAULT GETDATE(),
    Descripcion VARCHAR(255) NULL,
    PRIMARY KEY (IdTransaccion),
    FOREIGN KEY (IdCuentaOrigen) REFERENCES CuentasBancarias(IdCuenta),
    FOREIGN KEY (IdCuentaDestino) REFERENCES CuentasBancarias(IdCuenta),
    FOREIGN KEY (IdMoneda) REFERENCES Monedas(IdMoneda)
);
GO

-- -----------------------------------------------------
-- Tabla `Prestamos`
-- Almacena la información de los préstamos otorgados.
-- -----------------------------------------------------
CREATE TABLE Prestamos (
    IdPrestamo INT IDENTITY(1,1) NOT NULL,
    IdCliente INT NOT NULL,
    IdMoneda INT NOT NULL,
    MontoPrincipal DECIMAL(18, 4) NOT NULL,
    TasaInteres DECIMAL(5, 4) NOT NULL,
    PlazoMeses INT NOT NULL,
    FechaInicio DATE NOT NULL DEFAULT GETDATE(),
    FechaFinEstimada DATE NOT NULL,
    SaldoPendiente DECIMAL(18, 4) NOT NULL,
    EstadoPrestamo VARCHAR(50) NOT NULL,
    PRIMARY KEY (IdPrestamo),
    FOREIGN KEY (IdCliente) REFERENCES Clientes(IdCliente),
    FOREIGN KEY (IdMoneda) REFERENCES Monedas(IdMoneda)
);
GO

-- -----------------------------------------------------
-- Tabla `CuotasPrestamo`
-- Almacena el detalle de cada cuota de un préstamo.
-- -----------------------------------------------------
CREATE TABLE CuotasPrestamo (
    IdCuota INT IDENTITY(1,1) NOT NULL,
    IdPrestamo INT NOT NULL,
    NumeroCuota INT NOT NULL,
    FechaVencimiento DATE NOT NULL,
    MontoTotalCuota DECIMAL(18, 4) NOT NULL,
    MontoPagado DECIMAL(18, 4) NOT NULL DEFAULT 0.0000,
    EstadoCuota VARCHAR(50) NOT NULL DEFAULT 'PENDIENTE',
    PRIMARY KEY (IdCuota),
    UNIQUE (IdPrestamo, NumeroCuota),
    FOREIGN KEY (IdPrestamo) REFERENCES Prestamos(IdPrestamo)
);
GO