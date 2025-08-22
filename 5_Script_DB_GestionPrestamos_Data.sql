USE GestionPrestamos
GO

-- -----------------------------------------------------
-- Inserci�n de datos en `Monedas`
-- -----------------------------------------------------
INSERT INTO Monedas (Codigo, Nombre, Simbolo) VALUES ('USD', 'D�lar Estadounidense', '$');
INSERT INTO Monedas (Codigo, Nombre, Simbolo) VALUES ('EUR', 'Euro', '�');
INSERT INTO Monedas (Codigo, Nombre, Simbolo) VALUES ('PEN', 'Sol Peruano', 'S/');

-- -----------------------------------------------------
-- Inserci�n de datos en `TipoCuenta`
-- -----------------------------------------------------
INSERT INTO TipoCuenta (NombreTipo, Descripcion) VALUES ('Ahorros', 'Cuenta dise�ada para ahorrar dinero.');
INSERT INTO TipoCuenta (NombreTipo, Descripcion) VALUES ('Corriente', 'Cuenta para transacciones diarias y pagos.');
INSERT INTO TipoCuenta (NombreTipo, Descripcion) VALUES ('Empresarial', 'Cuenta para operaciones comerciales.');

-- -----------------------------------------------------
-- Inserci�n de datos en `Clientes`
-- -----------------------------------------------------
INSERT INTO Clientes (Nombre, Apellido, NumeroDocumento, Telefono, Email) VALUES ('Juan', 'P�rez', '12345678', '987654321', 'juanperez@mailcom');
INSERT INTO Clientes (Nombre, Apellido, NumeroDocumento, Telefono, Email) VALUES ('Ana', 'G�mez', '87654321', '912345678', 'anagomez@mailcom');
INSERT INTO Clientes (Nombre, Apellido, NumeroDocumento, Telefono, Email) VALUES ('Carlos', 'Rodr�guez', '99887766', '955555555', 'carlosr@mailcom');
INSERT INTO Clientes (Nombre, Apellido, NumeroDocumento, Telefono, Email) VALUES ('Mar�a', 'L�pez', '11223344', '944444444', 'marialopez@mailcom');

-- -----------------------------------------------------
-- Inserci�n de datos en `Usuarios`
-- -----------------------------------------------------
-- NOTA: Las contrase�as aqu� son de ejemplo y no seguras.
INSERT INTO Usuarios (Username, PasswordHash, IdCliente) VALUES ('juanp', 'hash123', 1);
INSERT INTO Usuarios (Username, PasswordHash, IdCliente) VALUES ('anag', 'hash456', 2);
INSERT INTO Usuarios (Username, PasswordHash, IdCliente) VALUES ('carlosr', 'hash789', 3);
INSERT INTO Usuarios (Username, PasswordHash, IdCliente) VALUES ('marial', 'hash101', 4);

-- -----------------------------------------------------
-- Inserci�n de datos en `Bancos`
-- -----------------------------------------------------
INSERT INTO Bancos (NombreBanco, CodigoBanco) VALUES ('Banco Central', 'BCE');
INSERT INTO Bancos (NombreBanco, CodigoBanco) VALUES ('Banco de la Naci�n', 'BN');
INSERT INTO Bancos (NombreBanco, CodigoBanco) VALUES ('Banco de Cr�dito', 'BCP');

-- -----------------------------------------------------
-- Inserci�n de datos en `CuentasBancarias`
-- -----------------------------------------------------
-- Cliente 1 (Juan P�rez)
INSERT INTO CuentasBancarias (IdCliente, IdTipoCuenta, IdMoneda, NumeroCuenta, Saldo) VALUES (1, 1, 1, 'C-1001-USD-01', 5000.00); -- Ahorros USD
INSERT INTO CuentasBancarias (IdCliente, IdTipoCuenta, IdMoneda, NumeroCuenta, Saldo) VALUES (1, 2, 3, 'C-1002-PEN-01', 10000.00); -- Corriente PEN

-- Cliente 2 (Ana G�mez)
INSERT INTO CuentasBancarias (IdCliente, IdTipoCuenta, IdMoneda, NumeroCuenta, Saldo) VALUES (2, 1, 1, 'C-2001-USD-01', 1500.50); -- Ahorros USD
INSERT INTO CuentasBancarias (IdCliente, IdTipoCuenta, IdMoneda, NumeroCuenta, Saldo) VALUES (2, 2, 2, 'C-2002-EUR-01', 250.75); -- Corriente EUR

-- Cliente 3 (Carlos Rodr�guez)
INSERT INTO CuentasBancarias (IdCliente, IdTipoCuenta, IdMoneda, NumeroCuenta, Saldo) VALUES (3, 1, 3, 'C-3001-PEN-01', 25000.00); -- Ahorros PEN

-- Cliente 4 (Mar�a L�pez)
INSERT INTO CuentasBancarias (IdCliente, IdTipoCuenta, IdMoneda, NumeroCuenta, Saldo) VALUES (4, 2, 3, 'C-4001-PEN-01', 750.25); -- Corriente PEN
INSERT INTO CuentasBancarias (IdCliente, IdTipoCuenta, IdMoneda, NumeroCuenta, Saldo) VALUES (4, 1, 1, 'C-4002-USD-01', 100.00); -- Ahorros USD

-- -----------------------------------------------------
-- Inserci�n de datos en `Transacciones`
-- -----------------------------------------------------
-- Transacciones entre clientes
INSERT INTO Transacciones (IdCuentaOrigen, IdCuentaDestino, TipoTransaccion, Monto, IdMoneda, Descripcion) VALUES (1, 3, 'Transferencia', 200.00, 1, 'Transferencia de Juan a Ana');
INSERT INTO Transacciones (IdCuentaOrigen, IdCuentaDestino, TipoTransaccion, Monto, IdMoneda, Descripcion) VALUES (2, 4, 'Transferencia', 500.00, 3, 'Transferencia de Juan a Maria');
INSERT INTO Transacciones (IdCuentaOrigen, IdCuentaDestino, TipoTransaccion, Monto, IdMoneda, Descripcion) VALUES (3, 1, 'Transferencia', 100.00, 1, 'Transferencia de Ana a Juan');

-- Transacciones de dep�sito y retiro
INSERT INTO Transacciones (IdCuentaOrigen, IdCuentaDestino, TipoTransaccion, Monto, IdMoneda, Descripcion) VALUES (NULL, 1, 'Dep�sito', 500.00, 1, 'Dep�sito en efectivo');
INSERT INTO Transacciones (IdCuentaOrigen, IdCuentaDestino, TipoTransaccion, Monto, IdMoneda, Descripcion) VALUES (2, NULL, 'Retiro', 100.00, 3, 'Retiro en cajero autom�tico');
INSERT INTO Transacciones (IdCuentaOrigen, IdCuentaDestino, TipoTransaccion, Monto, IdMoneda, Descripcion) VALUES (NULL, 5, 'Dep�sito', 5000.00, 3, 'Dep�sito inicial');
INSERT INTO Transacciones (IdCuentaOrigen, IdCuentaDestino, TipoTransaccion, Monto, IdMoneda, Descripcion) VALUES (5, NULL, 'Retiro', 1000.00, 3, 'Pago de servicios');

-- -----------------------------------------------------
-- Inserci�n de datos en `Prestamos`
-- -----------------------------------------------------
-- Pr�stamo 1 para Juan P�rez (IdCliente = 1)
INSERT INTO Prestamos (IdCliente, IdMoneda, MontoPrincipal, TasaInteres, PlazoMeses, FechaInicio, FechaFinEstimada, SaldoPendiente, EstadoPrestamo) VALUES (1, 3, 10000.00, 0.05, 12, '2025-01-01', '2025-12-31', 10500.00, 'ACTIVO');

-- Pr�stamo 2 para Ana G�mez (IdCliente = 2)
INSERT INTO Prestamos (IdCliente, IdMoneda, MontoPrincipal, TasaInteres, PlazoMeses, FechaInicio, FechaFinEstimada, SaldoPendiente, EstadoPrestamo) VALUES (2, 1, 5000.00, 0.04, 6, '2025-02-15', '2025-08-15', 5100.00, 'ACTIVO');

-- Pr�stamo 3 para Carlos Rodr�guez (IdCliente = 3)
INSERT INTO Prestamos (IdCliente, IdMoneda, MontoPrincipal, TasaInteres, PlazoMeses, FechaInicio, FechaFinEstimada, SaldoPendiente, EstadoPrestamo) VALUES (3, 3, 20000.00, 0.06, 24, '2025-03-20', '2027-03-20', 21200.00, 'ACTIVO');

-- -----------------------------------------------------
-- Inserci�n de datos en `CuotasPrestamo`
-- -----------------------------------------------------
-- Cuotas para el Pr�stamo 1 (IdPrestamo = 1)
INSERT INTO CuotasPrestamo (IdPrestamo, NumeroCuota, FechaVencimiento, MontoTotalCuota, MontoCapital, MontoInteres, SaldoPendiente, MontoPagado, EstadoCuota) VALUES (1, 1, '2025-02-01', 875.00, 833.33, 41.67, 9166.67, 875.00, 'PAGADO');
INSERT INTO CuotasPrestamo (IdPrestamo, NumeroCuota, FechaVencimiento, MontoTotalCuota, MontoCapital, MontoInteres, SaldoPendiente, MontoPagado, EstadoCuota) VALUES (1, 2, '2025-03-01', 875.00, 833.33, 41.67, 8333.34, 0.00, 'PENDIENTE');

-- Cuotas para el Pr�stamo 2 (IdPrestamo = 2)
INSERT INTO CuotasPrestamo (IdPrestamo, NumeroCuota, FechaVencimiento, MontoTotalCuota, MontoCapital, MontoInteres, SaldoPendiente, MontoPagado, EstadoCuota) VALUES (2, 1, '2025-03-15', 850.00, 833.33, 16.67, 4266.67, 850.00, 'PAGADO');
INSERT INTO CuotasPrestamo (IdPrestamo, NumeroCuota, FechaVencimiento, MontoTotalCuota, MontoCapital, MontoInteres, SaldoPendiente, MontoPagado, EstadoCuota) VALUES (2, 2, '2025-04-15', 850.00, 833.33, 16.67, 3433.34, 0.00, 'PENDIENTE');

-- Cuotas para el Pr�stamo 3 (IdPrestamo = 3)
INSERT INTO CuotasPrestamo (IdPrestamo, NumeroCuota, FechaVencimiento, MontoTotalCuota, MontoCapital, MontoInteres, SaldoPendiente, MontoPagado, EstadoCuota) VALUES (3, 1, '2025-04-20', 883.33, 833.33, 50.00, 20366.67, 883.33, 'PAGADO');
INSERT INTO CuotasPrestamo (IdPrestamo, NumeroCuota, FechaVencimiento, MontoTotalCuota, MontoCapital, MontoInteres, SaldoPendiente, MontoPagado, EstadoCuota) VALUES (3, 2, '2025-05-20', 883.33, 833.33, 50.00, 19533.34, 0.00, 'PENDIENTE');