-- -------------------------------------------------------
-- Inserción de datos de prueba
-- -------------------------------------------------------

USE GestionPrestamos;
GO

-- Datos para la tabla `Monedas`
INSERT INTO Monedas (Codigo, Nombre, Simbolo) VALUES
('USD', 'Dólar Estadounidense', '$'),
('EUR', 'Euro', '€'),
('MXN', 'Peso Mexicano', 'MXN$'),
('ARS', 'Peso Argentino', 'ARS$'),
('COP', 'Peso Colombiano', 'COP$'),
('CLP', 'Peso Chileno', 'CLP$'),
('PEN', 'Sol Peruano', 'S/');
GO

-- Datos para la tabla `Clientes`
INSERT INTO Clientes (Nombre, Apellido, NumeroDocumento, Telefono, Email) VALUES
('Juan', 'Pérez', '12345678A', '555-1234', 'juan.perez@email.com'),
('María', 'Gómez', '98765432B', '555-5678', 'maria.gomez@email.com'),
('Carlos', 'López', '11223344C', '555-9012', 'carlos.lopez@email.com'),
('Ana', 'Díaz', '55667788D', '555-3456', 'ana.diaz@email.com'),
('Luis', 'Ramírez', '99887766E', '555-7890', 'luis.ramirez@email.com'),
('Sofía', 'Fernández', '44556677F', '555-2345', 'sofia.fernandez@email.com'),
('Pedro', 'Gutiérrez', '22334455G', '555-6789', 'pedro.gutierrez@email.com'),
('Laura', 'Hernández', '88776655H', '555-1011', 'laura.hernandez@email.com'),
('José', 'Torres', '33445566I', '555-2233', 'jose.torres@email.com'),
('Elena', 'Vargas', '77889900J', '555-4455', 'elena.vargas@email.com'),
('Manuel', 'Castro', '11122233K', '555-6677', 'manuel.castro@email.com'),
('Lucía', 'Ortega', '44455566L', '555-8899', 'lucia.ortega@email.com'),
('Javier', 'Silva', '77788899M', '555-0011', 'javier.silva@email.com'),
('Marta', 'Herrera', '99900011N', '555-1122', 'marta.herrera@email.com'),
('David', 'Paredes', '33344455O', '555-3344', 'david.paredes@email.com'),
('Paula', 'Morales', '66677788P', '555-5566', 'paula.morales@email.com'),
('Fernando', 'Reyes', '00011122Q', '555-7788', 'fernando.reyes@email.com'),
('Diana', 'Sánchez', '33355577R', '555-9900', 'diana.sanchez@email.com'),
('Ricardo', 'Ruiz', '99911133S', '555-2211', 'ricardo.ruiz@email.com'),
('Patricia', 'Gil', '44466688T', '555-4433', 'patricia.gil@email.com');
GO

-- Datos para la tabla `Usuarios`
INSERT INTO Usuarios (Username, PasswordHash, IdCliente) VALUES
('juan.perez', 'hash_pass_12345', 1),
('maria.gomez', 'hash_pass_98765', 2),
('carlos.lopez', 'hash_pass_11223', 3),
('ana.diaz', 'hash_pass_55667', 4),
('luis.ramirez', 'hash_pass_99887', 5),
('sofia.fdez', 'hash_pass_44556', 6),
('pedro.gut', 'hash_pass_22334', 7),
('laura.hdz', 'hash_pass_88776', 8),
('jose.torres', 'hash_pass_33445', 9),
('elena.vargas', 'hash_pass_77889', 10),
('manuel.castro', 'hash_pass_11122', 11),
('lucia.ortega', 'hash_pass_44455', 12),
('javier.silva', 'hash_pass_77788', 13),
('marta.herrera', 'hash_pass_99900', 14),
('david.paredes', 'hash_pass_33344', 15),
('paula.morales', 'hash_pass_66677', 16),
('fernando.reyes', 'hash_pass_00011', 17),
('diana.sanchez', 'hash_pass_33355', 18),
('ricardo.ruiz', 'hash_pass_99911', 19),
('patricia.gil', 'hash_pass_44466', 20);
GO

-- Datos para las tablas `TipoCuenta` y `Bancos`
INSERT INTO TipoCuenta (NombreTipo, Descripcion) VALUES
('Ahorros', 'Cuenta diseñada para ahorrar dinero y generar intereses.'),
('Corriente', 'Cuenta para transacciones diarias y pagos.'),
('Crédito', 'Cuenta de crédito para financiar compras.'),
('Inversión', 'Cuenta para invertir en diversos instrumentos financieros.');
GO

INSERT INTO Bancos (NombreBanco, CodigoBanco) VALUES
('Banco Central', 'BNCEN'),
('Banco del Sur', 'BNSUR'),
('Banco del Norte', 'BNOR'),
('Banco del Oeste', 'BOEST');
GO

-- Datos para la tabla `CuentasBancarias`
INSERT INTO CuentasBancarias (IdCliente, IdTipoCuenta, IdMoneda, NumeroCuenta, Saldo, FechaApertura) VALUES
(1, 1, 1, 'C-USD-001', 1500.50, '2023-01-15'),
(1, 2, 3, 'C-MXN-001', 25000.75, '2023-02-20'),
(2, 1, 2, 'C-EUR-002', 800.00, '2022-11-10'),
(3, 2, 1, 'C-USD-003', 500.25, '2023-03-01'),
(4, 1, 5, 'C-COP-004', 1000000.00, '2023-05-05'),
(5, 3, 1, 'C-CR-005', 0.00, '2023-06-12'),
(6, 1, 4, 'C-ARS-006', 75000.00, '2023-07-01'),
(7, 2, 1, 'C-USD-007', 3000.00, '2023-08-22'),
(8, 1, 7, 'C-PEN-008', 2500.50, '2023-09-15'),
(9, 2, 6, 'C-CLP-009', 500000.00, '2023-10-18'),
(10, 1, 1, 'C-USD-010', 4500.00, '2023-11-25');
GO

-- Datos para la tabla `Transacciones`
INSERT INTO Transacciones (IdCuentaOrigen, IdCuentaDestino, TipoTransaccion, Monto, IdMoneda, Descripcion) VALUES
(1, 3, 'Transferencia', 200.00, 1, 'Transferencia a Carlos López'),
(NULL, 1, 'Depósito', 500.00, 1, 'Depósito en efectivo'),
(3, 7, 'Transferencia', 50.00, 1, 'Pago a Pedro Gutiérrez'),
(4, NULL, 'Retiro', 250.00, 5, 'Retiro en cajero automático'),
(NULL, 5, 'Depósito', 100.00, 1, 'Depósito de crédito'),
(7, 10, 'Transferencia', 300.00, 1, 'Transferencia a Elena Vargas'),
(8, NULL, 'Retiro', 500.00, 7, 'Retiro para compras');
GO

-- Datos para la tabla `Prestamos`
INSERT INTO Prestamos (IdCliente, IdMoneda, MontoPrincipal, TasaInteres, PlazoMeses, FechaInicio, FechaFinEstimada, SaldoPendiente, EstadoPrestamo) VALUES
(1, 1, 10000.00, 0.0500, 24, '2024-01-01', '2026-01-01', 9500.00, 'ACTIVO'),
(2, 1, 5000.00, 0.0800, 12, '2024-02-15', '2025-02-15', 0.00, 'PAGADO'),
(3, 3, 20000.00, 0.0650, 36, '2024-03-20', '2027-03-20', 18000.00, 'ACTIVO'),
(4, 5, 500000.00, 0.0750, 18, '2024-04-10', '2025-10-10', 450000.00, 'ACTIVO'),
(5, 1, 1500.00, 0.1000, 6, '2024-05-05', '2024-11-05', 1000.00, 'ACTIVO'),
(6, 4, 100000.00, 0.0900, 12, '2024-06-01', '2025-06-01', 100000.00, 'SOLICITADO');
GO

-- Datos para la tabla `CuotasPrestamo`
INSERT INTO CuotasPrestamo (IdPrestamo, NumeroCuota, FechaVencimiento, MontoTotalCuota, MontoPagado, EstadoCuota) VALUES
-- Cuotas para el préstamo 1 (IdPrestamo = 1)
(1, 1, '2024-02-01', 438.71, 438.71, 'PAGADA'),
(1, 2, '2024-03-01', 438.71, 438.71, 'PAGADA'),
(1, 3, '2024-04-01', 438.71, 438.71, 'PAGADA'),
(1, 4, '2024-05-01', 438.71, 0.00, 'PENDIENTE'),
(1, 5, '2024-06-01', 438.71, 0.00, 'PENDIENTE'),

-- Cuotas para el préstamo 2 (IdPrestamo = 2, ya pagado)
(2, 1, '2024-03-15', 439.16, 439.16, 'PAGADA'),
(2, 2, '2024-04-15', 439.16, 439.16, 'PAGADA'),
(2, 3, '2024-05-15', 439.16, 439.16, 'PAGADA'),

-- Cuotas para el préstamo 3 (IdPrestamo = 3)
(3, 1, '2024-04-20', 614.39, 614.39, 'PAGADA'),
(3, 2, '2024-05-20', 614.39, 614.39, 'PAGADA'),
(3, 3, '2024-06-20', 614.39, 0.00, 'PENDIENTE');
GO